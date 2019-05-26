using FluentAssertions;
using Xunit;

namespace Automation
{
    public class UnitTest1
    {
        [Fact]
        public void TestMethod1()
        {
            bool a = true;

            a.Should().Be(true);
        }
    }
}