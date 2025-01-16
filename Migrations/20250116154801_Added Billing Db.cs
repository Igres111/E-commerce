using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Migrations
{
    /// <inheritdoc />
    public partial class AddedBillingDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_billingInfos_Users_UserId",
                table: "billingInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_billingInfos",
                table: "billingInfos");

            migrationBuilder.RenameTable(
                name: "billingInfos",
                newName: "BillingInfos");

            migrationBuilder.RenameIndex(
                name: "IX_billingInfos_UserId",
                table: "BillingInfos",
                newName: "IX_BillingInfos_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillingInfos",
                table: "BillingInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillingInfos_Users_UserId",
                table: "BillingInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillingInfos_Users_UserId",
                table: "BillingInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillingInfos",
                table: "BillingInfos");

            migrationBuilder.RenameTable(
                name: "BillingInfos",
                newName: "billingInfos");

            migrationBuilder.RenameIndex(
                name: "IX_BillingInfos_UserId",
                table: "billingInfos",
                newName: "IX_billingInfos_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_billingInfos",
                table: "billingInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_billingInfos_Users_UserId",
                table: "billingInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
