using System.Collections;
using Playground1.Refactored.Exceptions;
using Playground1.Refactored.Models;
using Playground1.Refactored.Services;

namespace Playground1.Tests;

public sealed class CashMachineTests
{
    private sealed class InvalidDepositTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [-1];
            yield return [0];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    [Theory]
    [ClassData(typeof(InvalidDepositTestData))]
    public void ShouldThrowInvalidDeposit(int invalidCount)
    {
        var allDenominations = Enum.GetValues<Denomination>();

        ICashMachine testClient = new CashMachine();

        foreach (var denomination in allDenominations)
        {
            Assert.Throws<InvalidDepositCountException>(() => testClient.Deposit(denomination, invalidCount));
        }
    }

    [Fact]
    public void DepositShouldSucceed()
    {
        var allDenominations = Enum.GetValues<Denomination>();

        ICashMachine testClient = new CashMachine();

        foreach (var denomination in allDenominations)
        {
            var ex = Record.Exception(() => testClient.Deposit(denomination, 1));
            Assert.Null(ex);
        }
    }

    private sealed class InvalidWithdrawData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                new (int count, Denomination denomination)[]
                {

                }, //deposits
                50 //amountToWithdraw
            ];


            yield return
            [
                new (int count, Denomination denomination)[]
                {
                    (1, Denomination._50)
                }, //deposits
                100 //amountToWithdraw
            ];

            yield return
            [
                new (int count, Denomination denomination)[]
                {
                    (1, Denomination._50),
                    (1, Denomination._100)
                }, //deposits
                500 //amountToWithdraw
            ];

            yield return
            [
                new (int count, Denomination denomination)[]
                {
                    (1, Denomination._50),
                    (1, Denomination._100),
                    (1, Denomination._500)
                }, //deposits
                700 //amountToWithdraw
            ];

            yield return
            [
                new (int count, Denomination denomination)[]
                {
                    (1, Denomination._50),
                    (1, Denomination._100),
                    (1, Denomination._500),
                    (1, Denomination._1000)
                }, //deposits
                1700 //amountToWithdraw
            ];

            yield return
            [
                new (int count, Denomination denomination)[]
                {
                    (1, Denomination._50),
                    (2, Denomination._1000)
                }, //deposits
                2100 //amountToWithdraw
            ];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    [Theory]
    [ClassData(typeof(InvalidWithdrawData))]
    public void ShouldThrowNotEnoughMoney((int count, Denomination denomination)[] deposits, int amountToWithdraw)
    {
        ICashMachine testClient = new CashMachine();
        foreach (var (count, denomination) in deposits)
        {
            testClient.Deposit(denomination, count);
        }

        Assert.Throws<NoEnoughMoneyException>(() => testClient.Withdraw(amountToWithdraw));
    }

    private sealed class ValidWithdrawData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return
            [
                50, //amountToWithdraw
                new (Denomination denomination, int count)[]
                {
                    (Denomination._50, 1)
                }
            ];
            yield return
            [
                100, //amountToWithdraw
                new (Denomination denomination, int count)[]
                {
                    (Denomination._100, 1)
                }
            ];
            yield return
            [
                150, //amountToWithdraw
                new (Denomination denomination, int count)[]
                {
                    (Denomination._50, 1),
                    (Denomination._100, 1)
                }
            ];

            yield return
            [
                200, //amountToWithdraw
                new (Denomination denomination, int count)[]
                {
                    (Denomination._100, 2)
                }
            ];

            yield return
            [
                1_750, //amountToWithdraw
                new (Denomination denomination, int count)[]
                {
                    (Denomination._1000, 1),
                    (Denomination._500, 1),
                    (Denomination._100, 2),
                    (Denomination._50, 1)
                }
            ];

            yield return
            [
                7_900, //amountToWithdraw
                new (Denomination denomination, int count)[]
                {
                    (Denomination._1000, 7),
                    (Denomination._500, 1),
                    (Denomination._100, 4)
                }
            ];

            yield return
            [
                12_300, //amountToWithdraw
                new (Denomination denomination, int count)[]
                {
                    (Denomination._1000, 10),
                    (Denomination._500, 4),
                    (Denomination._100, 3)
                }
            ];

            yield return
            [
                16500, //amountToWithdraw
                new (Denomination denomination, int count)[]
                {
                    (Denomination._1000, 10),
                    (Denomination._500, 10),
                    (Denomination._100, 10),
                    (Denomination._50, 10)
                }
            ];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    [Theory]
    [ClassData(typeof(ValidWithdrawData))]
    public void WithdrawShouldSucceed(int amountToWithdraw, (Denomination denomination, int count)[] expectedBanknotes)
    {
        ICashMachine testClient = new CashMachine();
        testClient.Deposit(Denomination._50, 10);
        testClient.Deposit(Denomination._100, 10);
        testClient.Deposit(Denomination._500, 10);
        testClient.Deposit(Denomination._1000, 10);

        var dto = testClient.Withdraw(amountToWithdraw);
        foreach (var expectedBanknote in expectedBanknotes)
        {
            var banknoteCount = dto.Banknotes.GetValueOrDefault(expectedBanknote.denomination, defaultValue: -1);
            Assert.Equal(expectedBanknote.count, banknoteCount);
        }
    }

    [Fact]
    public void WithdrawTwiceShouldSucceed()
    {
        ICashMachine testClient = new CashMachine();
        testClient.Deposit(Denomination._50, 2);
        testClient.Deposit(Denomination._100, 3);

        var dto1 = testClient.Withdraw(200);
        Assert.Single(dto1.Banknotes);
        Assert.Equal(2, dto1.Banknotes[Denomination._100]);

        var dto2 = testClient.Withdraw(200);
        Assert.Equal(2, dto2.Banknotes.Count);
        Assert.Equal(1, dto2.Banknotes[Denomination._100]);
        Assert.Equal(2, dto2.Banknotes[Denomination._50]);
    }

    [Fact]
    public void WithdrawTwiceShouldThrow()
    {
        ICashMachine testClient = new CashMachine();
        testClient.Deposit(Denomination._50, 2);
        testClient.Deposit(Denomination._100, 3);

        var dto1 = testClient.Withdraw(250);
        Assert.Equal(2, dto1.Banknotes.Count);
        Assert.Equal(2, dto1.Banknotes[Denomination._100]);
        Assert.Equal(1, dto1.Banknotes[Denomination._50]);

        Assert.Throws<NoEnoughMoneyException>(() => testClient.Withdraw(200));
    }
}