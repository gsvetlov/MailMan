using System;
using System.Collections.Generic;
using System.Linq;

using MailMan.Data;
using MailMan.Models;

using NUnit.Framework;

namespace MailMan.Lib.Tests
{
    [TestFixture]
    public class DebugRepositorySenderTests
    {
        private DebugSenderRepository repo;
        
        [SetUp]
        public void InitRepo() => repo = new DebugSenderRepository(InitValidSenders());

        [Test]
        public void Ctor_WithNullList_Throws()
        {
            List<Sender> senders = null;

            Assert.Throws<ArgumentNullException>(() => new DebugSenderRepository(senders));
        }

        [Test]
        public void Ctor_WithDuplicatesList_Throws()
        {
            List<Sender> senders = InitDuplicateSenders();

            Assert.Throws<ArgumentOutOfRangeException>(() => new DebugSenderRepository(senders));
        }

        [Test]
        public void Ctor_WithZeroIdsList_Throws()
        {
            List<Sender> senders = InitZeroIdSenders();

            Assert.Throws<ArgumentOutOfRangeException>(() => new DebugSenderRepository(senders));
        }

       [Test]
        public void Ctor_WithValidList_ReturnsInstance()
        {
            List<Sender> senders = InitValidSenders();

            var repo = new DebugSenderRepository(senders);

            Assert.IsInstanceOf<DebugSenderRepository>(repo);
        }

        [Test]
        public void Add_Duplicate_ReturnsSame()
        {
            var newEntity = repo.GetAll().First();

            var actual = repo.Add(newEntity);

            Assert.AreSame(newEntity, actual);
        }

        [Test]
        public void Add_ZeroId_ReturnsSameWithNewId()
        {
            Sender newEntity = new() { Id = 0 };
            
            var expected = repo.GetAll().Max(e => e.Id) + 1;
            var instance = repo.Add(newEntity);
            var actual = instance.Id;

            Assert.AreEqual(expected, actual);
            Assert.AreSame(newEntity, instance);
        }

        [Test]
        public void Add_ValidId_ReturnsValidEntity()
        {
            int nextId = repo.GetAll().Max(e => e.Id) + 1;
            Sender newEntity = new() { Id = nextId };

            var instance = repo.Add(newEntity);
            var instanceId = instance.Id;

            Assert.AreEqual(nextId, instanceId);
            Assert.AreSame(newEntity, instance);
        }


        private static List<Sender> InitDuplicateSenders() => new()
        {
            new() { Id = 1 },
            new() { Id = 2 },
            new() { Id = 2 },
            new() { Id = 3 }
        };
        private static List<Sender> InitZeroIdSenders() => new()
        {
            new() { Id = 1 },
            new() { Id = 0 },
            new() { Id = 2 },
            new() { Id = 3 }
        };

        private static List<Sender> InitValidSenders() => new()
        {
            new() { Id = 1 },
            new() { Id = 4 },
            new() { Id = 3 },
            new() { Id = 8 }
        };
    }
}