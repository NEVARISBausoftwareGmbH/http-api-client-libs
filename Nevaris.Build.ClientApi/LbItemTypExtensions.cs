using System;
// ReSharper disable CollectionNeverUpdated.Global
#pragma warning disable CS1591

namespace Nevaris.Build.ClientApi;

/// <summary>
/// Erweiterungsmethoden für die Enumerationswerte von <see cref="LbItemTyp"/>
/// </summary>
public static class LbItemTypExtensions
{
    /// <summary>
    /// Konvertiert den Wert von <see cref="LbItemTyp"/> in <see cref="ObjectTyp"/>
    /// </summary>
    /// <param name="lbItemTyp"></param>
    /// <returns></returns>
    public static ObjectTyp? ToObjectTyp(this LbItemTyp? lbItemTyp) => lbItemTyp != null ? ToObjectTyp(lbItemTyp.Value) : null;

    /// <summary>
    /// Konvertiert den Wert von <see cref="LbItemTyp"/> in <see cref="ObjectTyp"/>
    /// </summary>
    /// <param name="lbItemTyp"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static ObjectTyp ToObjectTyp(this LbItemTyp lbItemTyp)
    {
        return lbItemTyp switch
        {
            LbItemTyp.Leistungsgruppe => ObjectTyp.OnLbLeistungsgruppe,
            LbItemTyp.Untergruppe => ObjectTyp.OnLbUntergruppe,
            LbItemTyp.Grundtext => ObjectTyp.OnLbGrundtext,
            LbItemTyp.Leistungsposition => ObjectTyp.OnLbLeistungsposition,
            LbItemTyp.Vorbemerkungsposition => ObjectTyp.OnLbVorbemerkungsposition,
            _ => throw new NotImplementedException()
        };
    }
}