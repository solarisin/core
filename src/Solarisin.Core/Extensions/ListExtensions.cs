namespace Solarisin.Core.Extensions;

/// <summary>
/// Class extensions for <see cref="List"/>.
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// Pop the element at the specified index from the list.
    /// </summary>
    /// <param name="list">This <see cref="List"/>.</param>
    /// <param name="index">The index of the item to pop.</param>
    /// <typeparam name="T">The type of the elements in this list.</typeparam>
    /// <returns>The popped element.</returns>
    public static T PopAt<T>(this List<T> list, int index)
    {
        var r = list[index];
        list.RemoveAt(index);
        return r;
    }

    /// <summary>
    /// Pop the first element from the list that matches the predicate.
    /// </summary>
    /// <param name="list">This <see cref="List"/>.</param>
    /// <param name="predicate">The predicate to match the element.</param>
    /// <typeparam name="T">The type of the elements in this list.</typeparam>
    /// <returns>The popped element.</returns>
    public static T PopFirst<T>(this List<T> list, Predicate<T> predicate)
    {
        var index = list.FindIndex(predicate);
        var r = list[index];
        list.RemoveAt(index);
        return r;
    }
    
    /// <summary>
    /// Pop the first element from the list that matches the predicate, or null.
    /// </summary>
    /// <param name="list">This <see cref="List"/>.</param>
    /// <param name="predicate">The predicate to match the element.</param>
    /// <typeparam name="T">The type of the elements in this list.</typeparam>
    /// <returns>The popped element, or null.</returns>
    public static T? PopFirstOrDefault<T>(this List<T?> list, Predicate<T?> predicate) where T : class
    {
        var index = list.FindIndex(predicate);
        if (index <= -1) return null;
        var r = list[index];
        list.RemoveAt(index);
        return r;
    }
}