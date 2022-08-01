namespace Validator.Test.PredicateBuilderExtensionsTest.Strings;

using NUnit.Framework;
using Validator.Test.PredicateBuilderExtensionsTest;

public class NotEquals
{
    [TestCase("abc", false)]
    [TestCase("cba", true)]
    [TestCase("", true)]
    [TestCase(null, true)]
    public void Test(string value, bool expected)
    {
        Assert.That(Helper.Validate<NotEqualsValidator, string>(value).Valid == expected);
    }

    public class NotEqualsValidator : Validator<TestModel<string>>
    {
        public NotEqualsValidator()
        {
            For(model => model.Value).NotEquals("abc", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
