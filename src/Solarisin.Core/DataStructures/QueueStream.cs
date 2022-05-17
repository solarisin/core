using System.Collections.Concurrent;
using System.Text;

namespace Solarisin.Core.DataStructures;

/// <summary>
///     This stream maintains data only until the data is read, then it is purged from the stream.
/// </summary>
public class QueueStream : Stream
{
    /// <summary>
    ///     Maintains the streams data. The ConcurrentQueue object provides an easy and efficient way to add and remove
    ///     data. Each item in the ConcurrentQueue represents each write to the stream, and every call to write translates
    ///     to an item in the channel.
    /// </summary>
    private readonly ConcurrentQueue<Chunk> _queue = new();

    /// <summary>
    ///     Always returns false, QueueStream is not seekable
    /// </summary>
    public override bool CanSeek => false;

    /// <summary>
    ///     Always returns true, QueueStream is always writeable
    /// </summary>
    public override bool CanWrite => true;

    /// <summary>
    ///     Always returns true, QueueStream is always readable
    /// </summary>
    public override bool CanRead => true;

    /// <summary>
    ///     Always returns 0
    /// </summary>
    public override long Position
    {
        //We're always at the start of the stream, because as the stream purges what we've read
        get => 0;
        set => throw new NotSupportedException($"{GetType().Name} is not seekable");
    }

    /// <summary>
    ///     Returns the number of bytes in the stream
    /// </summary>
    public override long Length => _queue.IsEmpty ? 0 : _queue.Sum(b => b.Data.Length - b.ChunkReadStartIndex);

    /// <summary>
    ///     Reads up to count bytes from the stream, and removes the read data from the stream.
    /// </summary>
    /// <param name="buffer">The buffer to read the data from</param>
    /// <param name="offset">The offset in the buffer to start reading from</param>
    /// <param name="count">The number of bytes to read</param>
    /// <returns>The number of bytes read</returns>
    public override int Read(byte[] buffer, int offset, int count)
    {
        ValidateBufferArgs(buffer, offset, count);

        var remainingBytes = count;
        var totalBytes = 0;

        // Read until we hit the requested count, or until we hav nothing left to read
        while (totalBytes <= count && !_queue.IsEmpty)
        {
            // Get first chunk from the queue
            if (!_queue.TryPeek(out var chunk)) continue;

            // Determine how much of the chunk there is left to read
            var unreadChunkLength = chunk.Data.Length - chunk.ChunkReadStartIndex;

            // Determine how much of the unread part of the chunk we can actually read
            var bytesToRead = Math.Min(unreadChunkLength, remainingBytes);

            if (bytesToRead > 0)
            {
                // Read from the chunk into the buffer
                Buffer.BlockCopy(chunk.Data, chunk.ChunkReadStartIndex, buffer, offset + totalBytes, bytesToRead);

                totalBytes += bytesToRead;
                remainingBytes -= bytesToRead;

                // If the entire chunk has been read, remove it
                if (chunk.ChunkReadStartIndex + bytesToRead >= chunk.Data.Length)
                    _queue.TryDequeue(out _);
                else
                    // Otherwise just update the chunk read start index, so we know where to start reading on the next call
                    chunk.ChunkReadStartIndex += bytesToRead;
            }
            else
            {
                break;
            }
        }

        return totalBytes;
    }

    /// <summary>
    ///     Writes byte array data to the queue stream
    /// </summary>
    /// <param name="buffer">Data to copy into the stream</param>
    /// <param name="offset">Offset into the buffer to start copying from</param>
    /// <param name="count">Number of bytes to copy</param>
    public override void Write(byte[] buffer, int offset, int count)
    {
        ValidateBufferArgs(buffer, offset, count);

        // We don't want to use the buffer passed in, as it could be altered by the caller
        var bufSave = new byte[count];
        Buffer.BlockCopy(buffer, offset, bufSave, 0, count);

        //Add the data to the queue
        _queue.Enqueue(new Chunk { ChunkReadStartIndex = 0, Data = bufSave });
    }

    /// <summary>
    ///     QueueStream is not seekable, always throws NotSupportedException
    /// </summary>
    /// <exception cref="NotSupportedException"></exception>
    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException($"{GetType().Name} is not seekable");
    }

    /// <summary>
    ///     QueueStream length cannot be set, always throws NotSupportedException
    /// </summary>
    /// <exception cref="NotSupportedException">Always thrown</exception>
    public override void SetLength(long value)
    {
        throw new NotSupportedException($"{GetType().Name} length can not be changed");
    }

    /// <summary>
    ///     Flushes the stream, this does nothing in this implementation
    /// </summary>
    public override void Flush()
    {
    }

    /// <summary>
    ///     Convert to byte and write the given data to the stream
    /// </summary>
    public void Enqueue<T>(T data) where T : IEnumerable<char>
    {
        // Convert the data to a byte array
        var buffer = Encoding.UTF8.GetBytes(data.ToArray());
        Write(buffer, 0, buffer.Length);
    }

    /// <summary>
    ///     Validates the buffer arguments before reading or writing
    /// </summary>
    /// <param name="buffer">Buffer to read or write</param>
    /// <param name="offset">Offset into the buffer to start reading or writing</param>
    /// <param name="count">Number of bytes to read or write</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the offset or count is less than 0</exception>
    /// <exception cref="ArgumentException">Thrown if the buffer length-offset is less than count</exception>
    private void ValidateBufferArgs(byte[] buffer, int offset, int count)
    {
        if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset), "offset must be non-negative");
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), "count must be non-negative");
        if (buffer.Length - offset < count)
            throw new ArgumentException("requested count exceeds available size", nameof(buffer));
    }

    /// <summary>
    ///     Represents a single write into the QueueStream. Each write is a separate chunk
    /// </summary>
    private sealed class Chunk
    {
        /// <summary>
        ///     As we read through the chunk, the start index will increment.  When we get to the end of the chunk,
        ///     we will remove the chunk
        /// </summary>
        public int ChunkReadStartIndex { get; set; }

        /// <summary>
        ///     Actual Data
        /// </summary>
        public byte[] Data { get; init; } = Array.Empty<byte>();
    }
}