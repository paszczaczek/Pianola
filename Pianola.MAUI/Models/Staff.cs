using System.Diagnostics.CodeAnalysis;
using Pianola.MAUI.Drawables;

namespace Pianola.MAUI.Models;

public class Staff
{
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    public class Position
    {
        public readonly int Number;

        private Position(int number) => Number = number;

        public static Position For(Pitch pitch, Octave octave, Clef clef)
        {
            var clefDefinition = ClefDrawable.ClefDefinitions[clef];
            var shiftNumber = ((int) clefDefinition.Pitch + (int) clefDefinition.Octave * 7) -
                           ((int) pitch + (int) octave * 7);
            var number = clefDefinition.StaffPosition.Number + shiftNumber;
            return new Staff.Position(number);
        }

        public static class Line
        {
            public static class Above
            {
                public static readonly Position Second = new Position(-4);
                public static readonly Position First = new Position(-2);

                public static Position Number(int lineNumber)
                {
                    if (lineNumber < 0)
                        throw new ArgumentException("Should be greater than 0", nameof(lineNumber));
                    var position = -lineNumber * 2;
                    return new(position);
                }
            }

            public static readonly Position Fifth = new Position(0);
            public static readonly Position Fourth = new Position(2);
            public static readonly Position Third = new Position(4);
            public static readonly Position Second = new Position(6);
            public static readonly Position First = new Position(8);

            public static Position Number(int lineNumber)
            {
                if (lineNumber is < 1 or > 5)
                    throw new ArgumentException("Should be between 1 and 5.", nameof(lineNumber));
                var position = (5 - lineNumber) * 2;
                return new(position);
            }

            public static class Below
            {
                public static readonly Position First = new Position(10);
                public static readonly Position Second = new Position(12);

                public static Position Number(int lineNumber)
                {
                    if (lineNumber < 0)
                        throw new ArgumentException("Should be greater than 0", nameof(lineNumber));
                    var position = 8 + lineNumber * 2;
                    return new(position);
                }
            }
        }

        public static class Space
        {
            public static class Above
            {
                public static readonly Position Second = new Position(-4);
                public static readonly Position First = new Position(-2);

                public static Position Number(int lineNumber)
                {
                    if (lineNumber < 0)
                        throw new ArgumentException("Should be greater than 0", nameof(lineNumber));
                    var position = -lineNumber * 2;
                    return new(position);
                }
            }

            public static readonly Position Fourth = new Position(1);
            public static readonly Position Third = new Position(3);
            public static readonly Position Second = new Position(5);
            public static readonly Position First = new Position(7);

            public static Position Number(int spaceNumber)
            {
                if (spaceNumber is < 1 or > 4)
                    throw new ArgumentException("Should be between 1 and 4.", nameof(spaceNumber));
                var position = (4 - spaceNumber) * 2 + 1;
                return new(position);
            }

            public static class Below
            {
                public static readonly Position First = new Position(8);
                public static readonly Position Second = new Position(10);

                public static Position Number(int spaceNumber)
                {
                    if (spaceNumber < 0)
                        throw new ArgumentException("Should be greater than 0", nameof(spaceNumber));
                    var position = 7 + spaceNumber * 2;
                    return new(position);
                }
            }
        }
    }
}