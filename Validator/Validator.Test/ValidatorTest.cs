namespace Validator.Test;

using Validator;
using NUnit.Framework;
using Validator.Models;
using Validator.Helpers;

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

        public static Result Validate(string value)
        {
            return Validate(new TestModel { Value = value });
        }
    }

    public class with_one_predicate
    {
        public class with_no_message : Common<with_no_message.TestValidator>
        {
            [Test]
            public void should_not_have_any_failures_when_predicate_pass()
            {
                Assert.That(Validate("asdf").Failures.Any(), Is.False);
            }

            [Test]
            public void should_have_failure_for_property_when_predicate_fail()
            {
                Assert.That(Validate("").ShouldHaveFailureForProperty(nameof(TestModel.Value)));
            }

            [Test]
            public void should_have_one_failure_when_predicate_fail()
            {
                Assert.That(Validate("").Failures.Count(), Is.EqualTo(1));
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
                Assert.That(Validate("asdf").Failures.Any(), Is.False);
            }

            [Test]
            public void should_have_failure_for_property_when_predicate_fail()
            {
                Assert.That(Validate("").ShouldHaveFailureForProperty(nameof(TestModel.Value)));
            }

            [Test]
            public void should_have_one_failure_when_predicate_fail()
            {
                Assert.That(Validate("").Failures.Count(), Is.EqualTo(1));
            }

            [Test]
            public void should_have_failure_with_message_when_predicate_fail()
            {
                Assert.That(Validate("").Failures.Single().Message == "message");
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
                    Assert.That(Validate(default(TestModel)).Failures.Any(), Is.False);
                }

                [Test]
                public void should_not_have_any_failures_when_condition_and_predicate_pass()
                {
                    Assert.That(Validate("asdf").Failures.Any(), Is.False);
                }

                [Test]
                public void should_have_one_failure_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate("").Failures.Count(), Is.EqualTo(1));
                }

                [Test]
                public void should_have_failure_for_property_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate("").ShouldHaveFailureForProperty(nameof(TestModel.Value)));
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
                    Assert.That(Validate(default(TestModel)).Failures.Any(), Is.False);
                }

                [Test]
                public void should_not_have_any_failures_when_condition_and_predicate_pass()
                {
                    Assert.That(Validate("asdf").Failures.Any(), Is.False);
                }

                [Test]
                public void should_have_one_failure_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate("").Failures.Count(), Is.EqualTo(1));
                }

                [Test]
                public void should_have_failure_for_property_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate("").ShouldHaveFailureForProperty(nameof(TestModel.Value)));
                }

                [Test]
                public void should_have_failure_with_message_when_condition_pass_and_predicate_fail()
                {
                    Assert.That(Validate("").Failures.Single().Message == "message");
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

    public class with_multiple_chained_predicates
    {
        public class with_no_messages : Common<with_no_messages.TestValidator>
        {
            [Test]
            public void should_not_have_any_failures_when_predicates_pass()
            {
                Assert.That(Validate("c").Failures.Any(), Is.False);
            }

            [Test]
            public void should_only_have_failure_for_first_predicate_when_both_fail()
            {
                Assert.That(Validate("abc").Failures.Single().Message == MessageHelper.NotEqualsMessage("abc"));
            }

            [Test]
            public void should_have_failure_for_second_predicate_when_first_pass_and_second_fail()
            {
                Assert.That(Validate("cba").Failures.Single().Message == null);
            }

            [Test]
            public void should_have_failure_for_property_when_first_predicate_fail()
            {
                Assert.That(Validate("abc").ShouldHaveFailureForProperty(nameof(TestModel.Value)));
            }

            [Test]
            public void should_have_failure_for_property_when_first_predicate_pass_and_second_fail()
            {
                Assert.That(Validate("cba").ShouldHaveFailureForProperty(nameof(TestModel.Value)));
            }

            public class TestValidator : Validator<TestModel>
            {
                public TestValidator()
                {
                    For(model => model.Value)
                        .NotEquals("abc")
                        .Custom(value => !value.Contains("b"));
                }
            }
        }

        public class with_messages : Common<with_messages.TestValidator>
        {
            [Test]
            public void should_not_have_any_failures_when_predicates_pass()
            {
                Assert.That(Validate("c").Failures.Any(), Is.False);
            }

            [Test]
            public void should_have_failure_for_property_when_first_predicate_fail()
            {
                Assert.That(Validate("abc").ShouldHaveFailureForProperty(nameof(TestModel.Value)));
            }

            [Test]
            public void should_have_failure_for_property_when_first_predicate_pass_and_second_fail()
            {
                Assert.That(Validate("cba").ShouldHaveFailureForProperty(nameof(TestModel.Value)));
            }

            [Test]
            public void should_have_failure_with_message_from_first_predicate_when_first_predicate_fail()
            {
                Assert.That(Validate("abc").Failures.Single().Message == "message1");
            }

            [Test]
            public void should_have_failure_with_message_from_second_predicate_when_second_predicate_fail()
            {
                Assert.That(Validate("cba").Failures.Single().Message == "message2");
            }

            public class TestValidator : Validator<TestModel>
            {
                public TestValidator()
                {
                    For(model => model.Value)
                        .NotEquals("abc")
                        .WithMessage("message1")
                        .Custom(value => !value.Contains("b"))
                        .WithMessage("message2");
                }
            }
        }

        public class with_one_condition
        {
            public class before : Common<before.TestValidator>
            {
                public class TestValidator : Validator<TestModel>
                {
                    public TestValidator()
                    {
                        For(model => model.Value)
                            .If(model => model is not null)
                            .NotEquals("abc")
                            .Custom(value => !value.Contains("b"));
                    }
                }
            }

            public class in_middle : Common<in_middle.TestValidator>
            {
                public class TestValidator : Validator<TestModel>
                {
                    public TestValidator()
                    {
                        For(model => model.Value)
                            .NotEquals("abc")
                            .If(model => model.Value != "cbab")
                            .Custom(value => !value.Contains("b"));
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


