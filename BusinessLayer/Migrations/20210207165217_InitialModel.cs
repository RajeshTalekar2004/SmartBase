using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartBase.BusinessLayer.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfo",
                table: "UserInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillMaster",
                table: "BillMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BilChq",
                table: "VoucherMaster",
                type: "nchar(16)",
                fixedLength: true,
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IgstAmount",
                table: "VoucherMaster",
                type: "money",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IgstId",
                table: "VoucherMaster",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfo",
                table: "UserInfo",
                column: "UserName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillMaster",
                table: "BillMaster",
                columns: new[] { "CompCode", "AccYear", "BillId" });

            migrationBuilder.CreateTable(
                name: "IgstMaster",
                columns: table => new
                {
                    IgstId = table.Column<int>(type: "int", nullable: false),
                    IgstDetail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IgstRate = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IgstMaster", x => x.IgstId);
                });

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "UserName",
                keyValue: "rajesh",
                columns: new[] { "UserPassword", "UserSalt" },
                values: new object[] { "RWt/KUFRqSKRBOKKJbZuPfwh9BKCqivii1hGSB3lOAs=", "kcKeWjK5qUe/iUyl+5Hv8Q==" });

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "UserName",
                keyValue: "suyash",
                columns: new[] { "UserPassword", "UserSalt" },
                values: new object[] { "RWt/KUFRqSKRBOKKJbZuPfwh9BKCqivii1hGSB3lOAs=", "kcKeWjK5qUe/iUyl+5Hv8Q==" });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherMaster_IgstId",
                table: "VoucherMaster",
                column: "IgstId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherMaster_IgstMaster",
                table: "VoucherMaster",
                column: "IgstId",
                principalTable: "IgstMaster",
                principalColumn: "IgstId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherMaster_IgstMaster",
                table: "VoucherMaster");

            migrationBuilder.DropTable(
                name: "IgstMaster");

            migrationBuilder.DropIndex(
                name: "IX_VoucherMaster_IgstId",
                table: "VoucherMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfo",
                table: "UserInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillMaster",
                table: "BillMaster");

            migrationBuilder.DropColumn(
                name: "IgstAmount",
                table: "VoucherMaster");

            migrationBuilder.DropColumn(
                name: "IgstId",
                table: "VoucherMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BilChq",
                table: "VoucherMaster",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(16)",
                oldFixedLength: true,
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfo_1",
                table: "UserInfo",
                column: "UserName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillMaster_1",
                table: "BillMaster",
                columns: new[] { "CompCode", "AccYear", "BillId" });

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "UserName",
                keyValue: "rajesh",
                columns: new[] { "UserPassword", "UserSalt" },
                values: new object[] { "16RK4oudoBW2oAYyytnpbo+ucmwyXAuAd3Ua8L+gfdQ=", "IXoYZgd271bZ39mTNvtXqg==" });

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "UserName",
                keyValue: "suyash",
                columns: new[] { "UserPassword", "UserSalt" },
                values: new object[] { "16RK4oudoBW2oAYyytnpbo+ucmwyXAuAd3Ua8L+gfdQ=", "IXoYZgd271bZ39mTNvtXqg==" });
        }
    }
}
