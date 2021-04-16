using FluentMigrator;
using FluentMigrator.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentering.WebAPI.Migrations
{
    [Migration(20210416_2)]
    public class Migration_20210416_2 : Migration
    {
        public override void Down()
        {
            Delete.Table("Renters");
        }

        public override void Up()
        {
            Create.Table("Renters")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
                .WithColumn("AccountId").AsInt32().NotNullable().ForeignKey("Accounts", "Id")
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("Nationality").AsString().NotNullable()
                .WithColumn("Ocupation").AsString().NotNullable()
                .WithColumn("MaritalStatus").AsInt32().NotNullable()
                .WithColumn("IdentityRG").AsString().NotNullable()
                .WithColumn("CPF").AsString().NotNullable()
                .WithColumn("Street").AsString().NotNullable()
                .WithColumn("Bairro").AsString().NotNullable()
                .WithColumn("Cidade").AsString().NotNullable()
                .WithColumn("CEP").AsString().NotNullable()
                .WithColumn("Estado").AsString().NotNullable()
                .WithColumn("SpouseFirstName").AsString().Nullable()
                .WithColumn("SpouseLastName").AsString().Nullable()
                .WithColumn("SpouseNationality").AsString().Nullable()
                .WithColumn("SpouseRG").AsString().Nullable()
                .WithColumn("SpouseCPF").AsString().Nullable();
        }
    }
}
