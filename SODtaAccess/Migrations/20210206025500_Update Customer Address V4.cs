using Microsoft.EntityFrameworkCore.Migrations;

namespace SODtaAccess.Migrations
{
    public partial class UpdateCustomerAddressV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderDetailOptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionDescription = table.Column<string>(maxLength: 200, nullable: false),
                    OrderDetailID = table.Column<int>(nullable: false),
                    OptionId = table.Column<int>(nullable: false),
                    AdditionalCost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetailOptions_ProductOptions_OptionId",
                        column: x => x.OptionId,
                        principalTable: "ProductOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetailOptions_OrderDetails_OrderDetailID",
                        column: x => x.OrderDetailID,
                        principalTable: "OrderDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailOptions_OptionId",
                table: "OrderDetailOptions",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailOptions_OrderDetailID",
                table: "OrderDetailOptions",
                column: "OrderDetailID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetailOptions");
        }
    }
}
