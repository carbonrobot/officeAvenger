using System;
using System.Data.Entity;
using System.Linq;
using OfficeAvenger.Domain.Data;

namespace OfficeAvenger.Tests.Fixtures
{
    public class TestDataFixture : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            base.Seed(context);
        }
    }
}