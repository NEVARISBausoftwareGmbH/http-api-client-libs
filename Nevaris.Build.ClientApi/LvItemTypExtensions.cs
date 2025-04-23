using System;
// ReSharper disable CollectionNeverUpdated.Global
#pragma warning disable CS1591

namespace Nevaris.Build.ClientApi;

/// <summary>
/// Erweiterungsmethoden für die Enumerationswerte von <see cref="LvItemTyp"/>
/// </summary>
public static class LvItemTypExtensions
{
    /// <summary>
    /// Konvertiert den Wert von <see cref="LvItemTyp"/> in <see cref="ObjectTyp"/>
    /// </summary>
    /// <param name="lvItemTyp"></param>
    /// <returns></returns>
    public static ObjectTyp? ToObjectTyp(this LvItemTyp? lvItemTyp) => lvItemTyp != null ? ToObjectTyp(lvItemTyp) : null;

    /// <summary>
    /// Konvertiert den Wert von <see cref="LvItemTyp"/> in <see cref="ObjectTyp"/>
    /// </summary>
    /// <param name="lvItemTyp"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static ObjectTyp ToObjectTyp(this LvItemTyp lvItemTyp)
    {
        return lvItemTyp switch
        {
            LvItemTyp.None => ObjectTyp.None,
            LvItemTyp.OnLeistungsposition => ObjectTyp.OnLvLeistungsposition,
            LvItemTyp.OnVorbemerkungsposition => ObjectTyp.OnLvVorbemerkungsposition,
            LvItemTyp.OnHauptgruppe => ObjectTyp.OnLvHauptgruppe,
            LvItemTyp.OnObergruppe => ObjectTyp.OnLvObergruppe,
            LvItemTyp.OnLeistungsgruppe => ObjectTyp.OnLvLeistungsgruppe,
            LvItemTyp.OnUntergruppe => ObjectTyp.OnLvUntergruppe,
            LvItemTyp.OnGrundtext => ObjectTyp.OnLvGrundtext,
            LvItemTyp.GaebLeistungsposition => ObjectTyp.GaebLeistungsposition,
            LvItemTyp.GaebZuschlagsposition => ObjectTyp.GaebZuschlagsposition,
            LvItemTyp.GaebAusfuehrungsbeschreibung => ObjectTyp.GaebAusfuehrungsbeschreibung,
            LvItemTyp.GaebAusfuehrungsbeschreibungText => ObjectTyp.GaebAusfuehrungsbeschreibungText,
            LvItemTyp.GaebHinweistext => ObjectTyp.GaebHinweistext,
            LvItemTyp.GaebTitel => ObjectTyp.GaebTitel,
            LvItemTyp.GaebLos => ObjectTyp.GaebLos,
            LvItemTyp.GaebUnterbeschreibung => ObjectTyp.GaebUnterbeschreibung,
            LvItemTyp.GaebZusatztext => ObjectTyp.GaebZusatztext,
            _ => throw new NotImplementedException(),
        };
    }
}
