using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nevaris.Build.ClientApi;

/// <summary>
/// Basisklasse aller Model-Klassen.
/// </summary>
public class BaseObject
{
    /// <summary>
    /// Dictionary, in dem beim Deserialisieren Feld-Werte abgespeichert werden, für die es keine entspechende
    /// Property im Model gibt. Ermöglicht ein versionierungstolerantes Serialisieren und Deserialisieren
    /// von Model-Objekten ohne Informationsverlust.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, object> AdditionalProperties { get; set; }
}