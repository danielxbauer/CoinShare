using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinShare.Api.Shared.Enums;
using CoinShare.Core.Models;

namespace CoinShare.Core.Logic.Impl
{
    public class MockDataLogic : IGroupLogic, IPersonLogic, ITransactionLogic
    {
        private int id = 1;

        private IList<Group> groups;
        private IList<Person> persons;
        private IList<Transaction> transactions;

        public MockDataLogic()
        {
            Guid group1Id = Guid.Parse("EB2FD2DB-2AC2-43B4-BD39-5CB218E91284");

            this.groups = new List<Group>
            {
                new Group { Id = group1Id, Name = "Kroatien Urlaub" }
            };

            this.persons = new List<Person>
            {
                new Person { Id = id++, GroupId = group1Id, Name = "Daniel" },
                new Person { Id = id++, GroupId = group1Id, Name = "Petra" },
                new Person { Id = id++, GroupId = group1Id, Name = "Max" }
            };

            this.transactions = new List<Transaction>
            {
                new Transaction { Id = id++, GroupId = group1Id, Text = "Pizza 1", Amount = 30m, PaidOn = DateTime.Now, PaidBy = 2, PaidFor = new[] { 1, 2, 3 }, TransactionType = TransactionType.Spending },
                new Transaction { Id = id++, GroupId = group1Id, Text = "Pizza 2", Amount = 30m, PaidOn = DateTime.Now, PaidBy = 2, PaidFor = new[] { 1, 2, 3 }, TransactionType = TransactionType.Spending },
                new Transaction { Id = id++, GroupId = group1Id, Text = "Pizza 3", Amount = 30m, PaidOn = DateTime.Now, PaidBy = 2, PaidFor = new[] { 1, 2, 3 }, TransactionType = TransactionType.Spending },
                new Transaction { Id = id++, GroupId = group1Id, Text = "Pizza 4", Amount = 30m, PaidOn = DateTime.Now, PaidBy = 2, PaidFor = new[] { 1, 2, 3 }, TransactionType = TransactionType.Spending },
                new Transaction { Id = id++, GroupId = group1Id, Text = "Pizza 5", Amount = 30m, PaidOn = DateTime.Now, PaidBy = 2, PaidFor = new[] { 1, 2, 3 }, TransactionType = TransactionType.Spending },
                new Transaction { Id = id++, GroupId = group1Id, Text = "Pizza 6", Amount = 30m, PaidOn = DateTime.Now, PaidBy = 2, PaidFor = new[] { 1, 2, 3 }, TransactionType = TransactionType.Spending },
                new Transaction { Id = id++, GroupId = group1Id, Text = "Pizza 7", Amount = 30m, PaidOn = DateTime.Now, PaidBy = 2, PaidFor = new[] { 1, 2, 3 }, TransactionType = TransactionType.Spending },
            };
        }

        public Task<Group> GetGroupByIdAsync(Guid id)
            => Task.FromResult(this.groups.First(g => g.Id == id));

        public Task<IEnumerable<PersonOverview>> GetPersonOverviewsAsync(Guid groupId)
        {
            var transactions = this.transactions.Where(g => g.GroupId == groupId).ToList();

            var overviews = this.persons.Where(p => p.GroupId == groupId)
                .Select(p => new PersonOverview
                {
                    PersonId = p.Id,
                    Paid = transactions
                        .Where(t => t.PaidBy == p.Id)
                        .Sum(t => t.Amount),
                    Consumed = transactions
                        .Where(t => t.PaidFor.Contains(p.Id))
                        .Select(t => t.Amount / t.PaidFor.Count())
                        .Sum()
                })
                .ToList();

            return Task.FromResult(overviews.AsEnumerable());
        }

        public Task<IEnumerable<Person>> GetPersonsByGroupIdAsync(Guid groupId)
            => Task.FromResult(this.persons.Where(p => p.GroupId == groupId));

        public Task<Transaction> GetTransactionByIdAsync(int id)
            => Task.FromResult(this.transactions.Where(t => t.Id == id).FirstOrDefault());

        public Task<IEnumerable<Transaction>> GetTransactionsByGroupIdAsync(Guid groupId)
            => Task.FromResult(this.transactions.Where(t => t.GroupId == groupId));

        public async Task SaveGroupAsync(Group group)
        {
            if (group.Id == Guid.Empty)
            {
                group.Id = Guid.NewGuid();
                this.groups.Add(group);
            }
            else
            {
                int index = this.groups
                    .Select((g, Index) => (g, Index))
                    .First(value => value.g.Id == group.Id)
                    .Index;

                this.groups.RemoveAt(index);
                this.groups.Insert(index, group);
            }

            await Task.Delay(1000); // Simulate database
        }

        public async Task SavePersonAsync(Person person)
        {
            if (person.Id == 0)
            {
                person.Id = id++;
                this.persons.Add(person);
            }
            else
            {
                int index = this.persons
                    .Select((p, Index) => (p, Index))
                    .First(value => value.p.Id == person.Id)
                    .Index;

                this.persons.RemoveAt(index);
                this.persons.Insert(index, person);
            }

            await Task.Delay(1000); // Simulate database
        }

        public async Task SaveTransactionAsync(Transaction transaction)
        {
            if (transaction.Id == 0)
            {
                transaction.Id = id++;
                this.transactions.Add(transaction);
            }
            else
            {
                int index = this.transactions
                    .Select((t, Index) => (t, Index))
                    .First(value => value.t.Id == transaction.Id)
                    .Index;

                this.transactions.RemoveAt(index);
                this.transactions.Insert(index, transaction);
            }

            await Task.Delay(1000); // Simulate database
        }
    }
}
