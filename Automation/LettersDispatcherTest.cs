using AutomationWithCSharp.Letters.Model;
using AutomationWithCSharp.Letters.Services;
using AutomationWithCSharp.Letters.Services.Interfaces;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Automation
{
    public class LettersDispatcherTest
    {
        private readonly Mock<IAgeValidator> ageValidator;
        private readonly Mock<IBadWordsValidator> badWordsValidator;
        private readonly Mock<INotificationSender> notificationSender;

        private readonly LettersDispatcher lettersDispatcher;

        public LettersDispatcherTest()
        {
            this.ageValidator = new Mock<IAgeValidator>();
            this.badWordsValidator = new Mock<IBadWordsValidator>();
            this.notificationSender = new Mock<INotificationSender>();

            this.lettersDispatcher = new LettersDispatcher(
                this.ageValidator.Object,
                this.badWordsValidator.Object,
                this.notificationSender.Object);
        }

        [Fact]
        public void Should_NotSendLetters_When_ThereAreNot()
        {
        }

        [Fact]
        public void Should_NotSendLetter_When_SenderIsNotOlderEnough()
        {
            //Setup
            var personNotOlderEnough = new Person
            {
                Age = AgeValidator.MinimunAgeToSendLetters - 1
            };

            var personOlderEnough = new Person
            {
                Age = AgeValidator.MinimunAgeToSendLetters + 1
            };

            var receiver = new Person
            {
            };

            var letterShouldNotBeSend = new Letter
            {
                Sender = personNotOlderEnough
            };

            var letterShouldBeSend = new Letter
            {
                Sender = personOlderEnough,
                Receivers = new List<Person>
                {
                    receiver
                },
                Body = "a body text"
            };

            var letters = new List<Letter>
            {
                letterShouldNotBeSend,
                letterShouldBeSend
            };

            this.ageValidator
                .Setup(x => x.IsNotOlderEnough(personNotOlderEnough))
                .Returns(true);

            this.ageValidator
                .Setup(x => x.IsNotOlderEnough(personOlderEnough))
                .Returns(false);

            this.badWordsValidator
                .Setup(x => x.ThereAreNotBadWords(letterShouldBeSend.Body))
                .Returns(true);

            //Act
            this.lettersDispatcher.Dispatch(letters);

            //Result
            var expectedLettersSent = new List<Letter>
            {
                letterShouldBeSend
            };

            this.ageValidator.Verify(
                x => x.IsNotOlderEnough(
                    It.Is<Person>(y => y.Equals(personNotOlderEnough))),
                Times.Once);

            this.ageValidator.Verify(
                x => x.IsNotOlderEnough(
                    It.Is<Person>(y => y.Equals(personOlderEnough))),
                Times.Once);

            this.badWordsValidator.Verify(
                x => x.ThereAreNotBadWords(
                    It.Is<string>(y => y == letterShouldBeSend.Body)),
                Times.Once);

            receiver.ReceivedLetters.Should().BeEquivalentTo(expectedLettersSent);
        }

        [Fact]
        public void Should_NotifyToTheSenderFamily_When_SenderIsNotOlderEnough()
        {
        }

        [Fact]
        public void Should_NotSendLetter_When_TheBodyOfItHasBadWords()
        {
        }

        [Fact]
        public void Should_SendLetterToReceivers_When_AllValidationsPass()
        {
        }
    }
}