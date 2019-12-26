using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Filtering;
using Filtering.Models;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    internal static class Program
    {
        private static readonly ContextExample Context = new ContextExample();
        private static readonly List<ExpressionFilter> Filters = new List<ExpressionFilter>
        {
            new ExpressionFilter
            {
                PropertyName = nameof(User.Age),
                Comparison = Comparison.Equal,
                Value = 20,
            },
            new ExpressionFilter
            {
                PropertyName = nameof(User.Username),
                Comparison = Comparison.StartsWith,
                Value = "jhan",
                Relation = Relation.And
            },
            new ExpressionFilter
            {
                PropertyName = nameof(User.Age),
                Comparison = Comparison.GreaterThanOrEqual,
                Value = 25,
                Relation = Relation.Or
            },
            new ExpressionFilter
            {
                PropertyName = nameof(User.FirstName),
                Comparison = Comparison.Equal,
                Value = "Peter",
                Relation = Relation.And
            }
        };

        public static async Task Main()
        {
            var efUsers = await GetMultipleEfCore();
            var efUser = await GetSingleEfCore();

            var efUserGeneric = await GetSingleEfCoreGeneric(Context.Users);
            var userGeneric = await GetSingleGeneric(Users);
        }

        private static async Task<IEnumerable<User>> GetMultipleEfCore()
        {
            // For multiple w/ EF Core
            var results = ExpressionCreator.Build<User>(Filters)
                .Select(Context.Users.Where)
                .Cast<IEnumerable<User>>()
                .ToList();

            var collectionSet = new List<User>();
            foreach (var result in results) collectionSet.AddRange(result);

            return await Task.FromResult(collectionSet.Distinct());
        }

        private static async Task<User> GetSingleEfCore()
        {
            var results = ExpressionCreator.Build<User>(Filters)
                .Select(Context.Users.Where)
                .Cast<IEnumerable<User>>()
                .ToList();

            var collectionSet = new List<User>();
            foreach (var result in results) collectionSet.AddRange(result);

            return await Task.FromResult(collectionSet.Distinct().Single());
        }

        private static async Task<T> GetSingleEfCoreGeneric<T>(IQueryable<T> collection)
        {
            var results = ExpressionCreator.Build<T>(Filters)
                .Select(collection.Where)
                .Cast<IEnumerable<T>>()
                .ToList();

            var collectionSet = new List<T>();
            foreach (var result in results) collectionSet.AddRange(result);

            return await Task.FromResult(collectionSet.Distinct().Single());
        }

        private static async Task<T> GetSingleGeneric<T>(IEnumerable<T> collection)
        {
            var results = ExpressionCreator.Build<T>(Filters)
                .Select(expression => collection.Where(expression.Compile()));

            var collectionSet = new List<T>();
            foreach (var result in results) collectionSet.AddRange(result);

            return await Task.FromResult(collectionSet.Distinct().Single());
        }

        private static readonly List<User> Users = new List<User>
        {
            new User
            {
                Username = "jhannes",
                FirstName = "Johan",
                LastName = "Hannes",
                Age = 20,
                CreationTimestamp = new DateTime(2019, 12, 22, 14, 1, 10),
                PurchaseAmountLifetime = 205.20m
            },
            new User
            {
                Username = "pcannes",
                FirstName = "Peter",
                LastName = "Cannes",
                Age = 30,
                CreationTimestamp = new DateTime(2018, 12, 22, 14, 1, 10),
                PurchaseAmountLifetime = 2002.20m
            },
            new User
            {
                Username = "crannes",
                FirstName = "Calr",
                LastName = "rannes",
                Age = 45,
                CreationTimestamp = new DateTime(2000, 12, 22, 14, 1, 10),
                PurchaseAmountLifetime = 0.20m
            }
        };
    }
}