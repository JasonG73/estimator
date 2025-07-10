using System;

namespace Estimator.Data.Entities;

public static class Rebar
{

    public enum SizeSystem
    {
        Metric = 1,
        Imperial = 2
    }
    public enum Type
    {
        FootingLengths = 10,
        FootingCrossPieces = 11,
        FootingDowels = 12,
        PadStraightBars = 20,
        PadDowels = 21,

    };

    public enum MetricSize
    {
        TenM = 10,
        FifteenM = 15,
        TwentyM = 20,
        TwentyFiveM = 25,
    };

    public enum ImperialSize
    {
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eigth = 8,
    }

    public enum PricingMethod
    {
        InstallOnly = 1,
        EachAndInstall = 2,
        PerFootInstall = 3


    }

    public static List<Type> FootingReinforcingPieces = [Type.FootingCrossPieces, Type.FootingDowels, Type.FootingLengths];

}
