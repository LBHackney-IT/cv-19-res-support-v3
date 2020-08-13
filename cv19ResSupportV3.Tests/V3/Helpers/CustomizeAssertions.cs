using System;
using FluentAssertions;

namespace cv19ResSupportV3.Tests.V3.Helper
{
    public static class CustomizeAssertions
    {
        public static void ApproximationDateTime()
        {
            AssertionOptions.AssertEquivalencyUsing(options =>
            {
                options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTime>();
                options.Using<DateTimeOffset>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTimeOffset>();
                return options;
            });
        }
    }
}
