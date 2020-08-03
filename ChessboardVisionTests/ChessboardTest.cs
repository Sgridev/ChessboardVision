using ChessboardVision.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace ChessboardVisionTests
{
    public class ChessboardTest
    {
        private readonly List<string> _chessboardSquares;
        public ChessboardTest()
        {
            _chessboardSquares = new List<string> {   "A1","A2","A3","A4","A5","A6","A7","A8","B1","B2","B3","B4","B5","B6","B7","B8","C1","C2","C3","C4","C5","C6","C7","C8","D1","D2","D3","D4","D5","D6","D7","D8","E1","E2","E3","E4","E5","E6","E7","E8","F1","F2","F3","F4","F5","F6","F7","F8","G1","G2","G3","G4","G5","G6","G7","G8","H1","H2","H3","H4","H5","H6","H7","H8"};
        }

        [Theory]
        [Repeat(1000)]
        public void GetRandomSquareReturnsASquare()
        {
            Assert.Contains(Chessboard.GetRandomSquare(), _chessboardSquares);
        }

        [Theory]
        [InlineData("A2")]
        [InlineData("A4")]
        [InlineData("A6")]
        [InlineData("A8")]
        [InlineData("B1")]
        [InlineData("B3")]
        [InlineData("B5")]
        [InlineData("B7")]
        [InlineData("C2")]
        [InlineData("C4")]
        [InlineData("C6")]
        [InlineData("C8")]
        [InlineData("D1")]
        [InlineData("D3")]
        [InlineData("D5")]
        [InlineData("D7")]
        [InlineData("E2")]
        [InlineData("E4")]
        [InlineData("E6")]
        [InlineData("E8")]
        [InlineData("F1")]
        [InlineData("F3")]
        [InlineData("F5")]
        [InlineData("F7")]
        [InlineData("G2")]
        [InlineData("G4")]
        [InlineData("G6")]
        [InlineData("G8")]
        [InlineData("H1")]
        [InlineData("H3")]
        [InlineData("H5")]
        [InlineData("H7")]

        public void IsColorCorrectWhiteSquares(string square)
        {
            Assert.True(Chessboard.IsColorCorrect(square, true));
        }

        [Theory]
        [InlineData("A2")]
        [InlineData("A4")]
        [InlineData("A6")]
        [InlineData("A8")]
        [InlineData("B1")]
        [InlineData("B3")]
        [InlineData("B5")]
        [InlineData("B7")]
        [InlineData("C2")]
        [InlineData("C4")]
        [InlineData("C6")]
        [InlineData("C8")]
        [InlineData("D1")]
        [InlineData("D3")]
        [InlineData("D5")]
        [InlineData("D7")]
        [InlineData("E2")]
        [InlineData("E4")]
        [InlineData("E6")]
        [InlineData("E8")]
        [InlineData("F1")]
        [InlineData("F3")]
        [InlineData("F5")]
        [InlineData("F7")]
        [InlineData("G2")]
        [InlineData("G4")]
        [InlineData("G6")]
        [InlineData("G8")]
        [InlineData("H1")]
        [InlineData("H3")]
        [InlineData("H5")]
        [InlineData("H7")]
        public void IsColorWrongWhiteSquares(string square)
        {
            Assert.False(Chessboard.IsColorCorrect(square, false));
        }

        [Theory]
        [InlineData("A1")]
        [InlineData("A3")]
        [InlineData("A5")]
        [InlineData("A7")]
        [InlineData("B2")]
        [InlineData("B4")]
        [InlineData("B6")]
        [InlineData("B8")]
        [InlineData("C1")]
        [InlineData("C3")]
        [InlineData("C5")]
        [InlineData("C7")]
        [InlineData("D2")]
        [InlineData("D4")]
        [InlineData("D6")]
        [InlineData("D8")]
        [InlineData("E1")]
        [InlineData("E3")]
        [InlineData("E5")]
        [InlineData("E7")]
        [InlineData("F2")]
        [InlineData("F4")]
        [InlineData("F6")]
        [InlineData("F8")]
        [InlineData("G1")]
        [InlineData("G3")]
        [InlineData("G5")]
        [InlineData("G7")]
        [InlineData("H2")]
        [InlineData("H4")]
        [InlineData("H6")]
        [InlineData("H8")]
        public void IsColorRightDarkSquares(string square)
        {
            Assert.True(Chessboard.IsColorCorrect(square, false));
        }

        [Theory]
        [InlineData("A1")]
        [InlineData("A3")]
        [InlineData("A5")]
        [InlineData("A7")]
        [InlineData("B2")]
        [InlineData("B4")]
        [InlineData("B6")]
        [InlineData("B8")]
        [InlineData("C1")]
        [InlineData("C3")]
        [InlineData("C5")]
        [InlineData("C7")]
        [InlineData("D2")]
        [InlineData("D4")]
        [InlineData("D6")]
        [InlineData("D8")]
        [InlineData("E1")]
        [InlineData("E3")]
        [InlineData("E5")]
        [InlineData("E7")]
        [InlineData("F2")]
        [InlineData("F4")]
        [InlineData("F6")]
        [InlineData("F8")]
        [InlineData("G1")]
        [InlineData("G3")]
        [InlineData("G5")]
        [InlineData("G7")]
        [InlineData("H2")]
        [InlineData("H4")]
        [InlineData("H6")]
        [InlineData("H8")]
        public void IsColorWrongDarkSquares(string square)
        {
            Assert.False(Chessboard.IsColorCorrect(square, true));
        }
    }

    public class RepeatAttribute : DataAttribute
    {
        private readonly int _count;

        public RepeatAttribute(int count)
        {
            if (count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(count),
                      "Repeat count must be greater than 0.");
            }
            _count = count;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return Enumerable.Repeat(new object[0], _count);
        }
    }
}
