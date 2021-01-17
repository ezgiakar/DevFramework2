using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable(@"Users", @"dbo");
            HasKey(x => x.Id);

            Property(x => x.Email).HasColumnName("Email");
            Property(x => x.LastName).HasColumnName("LasName");
            Property(x => x.Password).HasColumnName("Password");
            Property(x => x.UserName).HasColumnName("UserName");
            Property(x => x.FirstName).HasColumnName("FirstName");
        }
    }
}
