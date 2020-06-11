using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AisCodeChallenge.Tests.Extensions
{

    /// <summary>
    /// Test extensions
    /// </summary>
    public static class TestExtensions
    {
        public static T ShouldEqual<T>(this T actual, object expected)
        {
            Assert.AreEqual(expected, actual);
            return actual;
        }

        public static T ShouldNotEqual<T>(this T actual, object expected)
        {
            Assert.AreNotEqual(expected, actual);
            return actual;
        }
    }
}
