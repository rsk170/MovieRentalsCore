using Microsoft.EntityFrameworkCore.Migrations;

namespace VidlyCore.Entities.Migrations
{
    public partial class AddMembershipTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MembershipTypes",
                columns: new[] { "Id", "DiscountRate", "DurationInMonths", "Name", "SignUpFee" },
                values: new object[,]
                {
                    { (byte)1, (byte)0, (byte)0, "Pay as you go", (short)0 },
                    { (byte)2, (byte)10, (byte)1, "Monthly", (short)30 },
                    { (byte)3, (byte)15, (byte)3, "Quarterly", (short)90 },
                    { (byte)4, (byte)20, (byte)12, "Annual", (short)300 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: (byte)2);

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: (byte)3);

            migrationBuilder.DeleteData(
                table: "MembershipTypes",
                keyColumn: "Id",
                keyValue: (byte)4);
        }
    }
}
