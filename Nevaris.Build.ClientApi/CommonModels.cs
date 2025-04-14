using System.Collections.Generic;
using Newtonsoft.Json;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Nevaris.Build.ClientApi;

/// <summary>
/// Basisklasse aller Model-Klassen.
/// </summary>
public class BaseObject
{
    protected BaseObject() { }

    /// <summary>
    /// Dictionary, in dem beim Deserialisieren Feld-Werte abgespeichert werden, für die es keine entspechende
    /// Property im Model gibt. Ermöglicht ein versionierungstolerantes Serialisieren und Deserialisieren
    /// von Model-Objekten ohne Informationsverlust.
    /// </summary>
    [JsonExtensionData]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public Dictionary<string, object>? AdditionalProperties { get; set; }
}