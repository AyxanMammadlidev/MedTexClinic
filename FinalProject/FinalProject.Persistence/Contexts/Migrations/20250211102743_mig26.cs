using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Taxes",
                table: "Payrolls");

            migrationBuilder.AddColumn<float>(
                name: "InsuranceRate",
                table: "Payrolls",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateOnly>(
                name: "PaymentTime",
                table: "Payrolls",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<float>(
                name: "TaxRate",
                table: "Payrolls",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Salary",
                table: "Doctors",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsuranceRate",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "PaymentTime",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "TaxRate",
                table: "Payrolls");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Doctors");

            migrationBuilder.AddColumn<decimal>(
                name: "Taxes",
                table: "Payrolls",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
