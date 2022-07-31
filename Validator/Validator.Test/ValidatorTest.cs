namespace Validator.Test;

using Validator;
using NUnit.Framework;
using Validator.Models;

public class ValidatorTest
{
    // with one predicate
    // with one predicate and message
    // with one predicate and one condition
    // with one predicate and one condition and message

    // with multiple chained predicates
    // with multiple chained predicates and messages
    // with multiple chained predicates and one condition before
    // with multiple chained predicates and one condition in middle

    // with multiple non chained predicates

    // with nested validator
    // with nested validator and one condition

    public abstract class Common<TValidator> where TValidator : IValidator<TestModel>, new()
    {
        public static Result Validate(TestModel model)
        {
            return new TValidator().Validate(model);
        }
    }

    public class with_one_predicate
    {
        public class with_no_message : Common<with_no_message.TestValidator>
        {
            [Test]
            public void should_not_have_any_failures_when_predicate_pass()
            {
                Assert.That(Validate(new TestModel { Value = "asdf" }).Failures.Any(), Is.False);
            }

            [Test]
            public void should_have_failure_for_property_when_predicate_fail()
            {
                Assert.That(Validate(new TestModel { Value = "" }).ShouldHaveFailureForProperty(nameof(TestModel.Value)));
            }

            [Test]
            public void should_have_one_failure_when_predicate_fail()
            {
                Assert.That(Validate(new TestModel { Value = "" }).Failures.Count(), Is.EqualTo(1));
            }

            public class TestValidator : Validator<TestModel>
            {
                public TestValidator()
                {
                    For(model => model.Value).NotEmpty();
                }
            }
        }

        public class with_message : Common<with_message.TestValidator>
        {
            [Test]
            public void should_not_have_any_failures_when_predicate_pass()
            {
                Assert.That(Validate(new TestModel { Value = "asdf" }).Failures.Any(), Is.False);
            }

            [Test]
            public void should_have_failure_for_property_when_predicate_fail()
            {
                Assert.That(Validate(new TestModel { Value = "" }).ShouldHaveFailureForProperty(nameof(TestModel.Value)));
            }

            [Test]
            public void should_have_one_failure_when_predicate_fail()
            {
                Assert.That(Validate(new TestModel { Value = "" }).Failures.Count(), Is.EqualTo(1));
            }

            [Test]
            public void should_have_failure_with_message_when_predicate_fail()
            {
                Assert.That(Validate(new TestModel { Value = "" }).Failures.Single().Message == "message");
            }

            public class TestValidator : Validator<TestModel>
            {
                public TestValidator()
                {
                    For(model => model.Value).NotEmpty().WithMessage("message");
                }
            }
        }

        public class with_one_condition
        {
            public class with_no_message : Common<with_no_message.TestValidator>
            {
                [Test]
                public void should_not_have_any_failures_when_condition_fail()
                {
                    Assert.That(Validate(null).Failures.Any(), Is.False);
                }

                [Test]
                public void should_not_have_any_failures_when_condition_and_predicate_pass()
                {
                    Assert.That(Validate(new TestModel { Value = "asdf" }).Failures.Any(), Is.False);
                }

                [Test]
                public void should_have_one_failure_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate(new TestModel { Value = "" }).Failures.Count(), Is.EqualTo(1));
                }

                [Test]
                public void should_have_failure_for_property_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate(new TestModel { Value = "" }).ShouldHaveFailureForProperty(nameof(TestModel.Value)));
                }

                public class TestValidator : Validator<TestModel>
                {
                    public TestValidator()
                    {
                        For(model => model.Value).If(model => model is not null).NotEmpty();
                    }
                }
            }

            public class with_message : Common<with_message.TestValidator>
            {
                [Test]
                public void should_not_have_any_failures_when_condition_fail()
                {
                    Assert.That(Validate(null).Failures.Any(), Is.False);
                }

                [Test]
                public void should_not_have_any_failures_when_condition_and_predicate_pass()
                {
                    Assert.That(Validate(new TestModel { Value = "asdf" }).Failures.Any(), Is.False);
                }

                [Test]
                public void should_have_one_failure_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate(new TestModel { Value = "" }).Failures.Count(), Is.EqualTo(1));
                }

                [Test]
                public void should_have_failure_for_property_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate(new TestModel { Value = "" }).ShouldHaveFailureForProperty(nameof(TestModel.Value)));
                }

                [Test]
                public void should_have_failure_with_message_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate(new TestModel { Value = "" }).Failures.Single().Message == "message");
                }

                public class TestValidator : Validator<TestModel>
                {
                    public TestValidator()
                    {
                        For(model => model.Value).If(model => model is not null).NotEmpty().WithMessage("message");
                    }
                }
            }
        }
    }
}

public static class ValidatorTestExtensions
{
    public static bool ShouldHaveFailureForProperty(this Result result, string propertyName)
    {
        return result.Failures.FirstOrDefault(failure => failure.PropertyName == propertyName) != default;
    }
}

public class TestModel
{
    public string Value { get; set; }
    public TestSubModel NestedModel { get; set; }
}

public class TestSubModel
{
    public string Value { get; set; }
}


