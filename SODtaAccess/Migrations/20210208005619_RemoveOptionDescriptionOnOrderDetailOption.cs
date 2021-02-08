using Microsoft.EntityFrameworkCore.Migrations;

namespace SODtaAccess.Migrations
{
    public partial class RemoveOptionDescriptionOnOrderDetailOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionDescription",
                table: "OrderDetailOptions");

            
            var sp_getAllDetailOption = @"
                                       ALTER PROCEDURE SP_GetAllDetailOption 
                                        @OrderId int
                                        AS
                                        BEGIN
                                            SELECT OrderDetailOptions.Id
                                            ,OrderDetailID
                                            ,OptionId
                                            ,OrderDetailOptions.AdditionalCost
                                            , ProductOptions.OptionDescription
                                            FROM OrderDetailOptions, OrderDetails, ProductOptions
                                            Where OrderDetailOptions.OrderDetailID = OrderDetails.Id 
                                            and ProductOptions.Id = OrderDetailOptions.OptionId
                                            and OrderDetails.OrderID = @OrderId
                                        END     

            ";
            migrationBuilder.Sql(sp_getAllDetailOption);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OptionDescription",
                table: "OrderDetailOptions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
