﻿using System.Reflection;
using Serilog;
using Serilog.Configuration;

namespace Solarisin.Core.Extensions.Logging;

/// <summary>
///     Extends <see cref="Serilog.LoggerConfiguration" /> to add enrichment options during logger configuration.
/// </summary>
public static class LoggerConfigurationExtensions
{
    /// <summary>
    ///     Enrich log events with an AssemblyName property containing the current <see cref="AssemblyName.Name" />.
    /// </summary>
    /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public static LoggerConfiguration WithAssemblyName(this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
        return enrichmentConfiguration.WithAssemblyName(assembly);
    }

    /// <summary>
    ///     Enrich log events with an AssemblyName property containing the current <see cref="AssemblyName.Name" /> of T.
    /// </summary>
    /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public static LoggerConfiguration WithAssemblyName<T>(this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        var assembly = typeof(T).Assembly;
        return enrichmentConfiguration.WithAssemblyName(assembly);
    }

    /// <summary>
    ///     Enrich log events with an AssemblyVersion property containing the current <see cref="AssemblyName.Version" />.
    /// </summary>
    /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
    /// <param name="useSemVer">Use semantic versioning.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public static LoggerConfiguration WithAssemblyVersion(this LoggerEnrichmentConfiguration enrichmentConfiguration,
        bool useSemVer = false)
    {
        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
        return enrichmentConfiguration.WithAssemblyVersion(assembly, useSemVer);
    }

    /// <summary>
    ///     Enrich log events with an AssemblyVersion property containing the current <see cref="AssemblyName.Version" /> of T.
    /// </summary>
    /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
    /// <param name="useSemVer">Use semantic versioning.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public static LoggerConfiguration WithAssemblyVersion<T>(this LoggerEnrichmentConfiguration enrichmentConfiguration,
        bool useSemVer = false)
    {
        var assembly = typeof(T).Assembly;
        return enrichmentConfiguration.WithAssemblyVersion(assembly, useSemVer);
    }

    /// <summary>
    ///     Enrich log events with an AssemblyVersion property containing the current
    ///     <see cref="AssemblyInformationalVersionAttribute.InformationalVersion" /> from the assembly.
    /// </summary>
    /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public static LoggerConfiguration WithAssemblyInformationalVersion(
        this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
        return enrichmentConfiguration.WithAssemblyInformationalVersion(assembly);
    }

    /// <summary>
    ///     Enrich log events with an AssemblyVersion property containing the current
    ///     <see cref="AssemblyInformationalVersionAttribute.InformationalVersion" /> from the assembly of T.
    /// </summary>
    /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
    /// <returns>Configuration object allowing method chaining.</returns>
    public static LoggerConfiguration WithAssemblyInformationalVersion<T>(
        this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        var assembly = typeof(T).Assembly;
        return enrichmentConfiguration.WithAssemblyInformationalVersion(assembly);
    }

    private static LoggerConfiguration WithAssemblyName(this LoggerEnrichmentConfiguration enrichmentConfiguration,
        Assembly assembly)
    {
        return enrichmentConfiguration.WithProperty("AssemblyName", assembly.GetName().Name ?? string.Empty);
    }

    private static LoggerConfiguration WithAssemblyVersion(this LoggerEnrichmentConfiguration enrichmentConfiguration,
        Assembly assembly, bool useSemVer)
    {
        var version = assembly.GetName().Version;
        var versionString = string.Empty;
        if (version != null)
            versionString = useSemVer ? $"{version.Major}.{version.Minor}.{version.Build}" : $"{version}";
        return enrichmentConfiguration.WithProperty("AssemblyVersion", versionString);
    }

    private static LoggerConfiguration WithAssemblyInformationalVersion(
        this LoggerEnrichmentConfiguration enrichmentConfiguration, Assembly assembly)
    {
        var versionString = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion ?? string.Empty;
        return enrichmentConfiguration.WithProperty("AssemblyVersion", versionString);
    }
}