using Microsoft.EntityFrameworkCore.Migrations;

namespace SODtaAccess.Migrations
{
    public partial class CreateSP_GetAllDetailOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp_getAllDetailOption = @"
                                        CREATE PROCEDURE SP_GetAllDetailOption 
                                        @OrderId int
                                        AS
                                        BEGIN
                                        SELECT OrderDetailOptions.Id
                                                ,OptionDescription
                                                ,OrderDetailID
                                                ,OptionId
                                                ,AdditionalCost
                                        FROM OrderDetailOptions, OrderDetails
                                        Where OrderDetailOptions.OrderDetailID = OrderDetails.Id 
                                        and OrderDetails.OrderID = @OrderId
                                        END    

            ";
            migrationBuilder.Sql(sp_getAllDetailOption);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropSelectNewProduct = "IF OBJECT_ID('SP_GetAllDetailOption','P') IS NOT NULL DROP PROC SP_GetAllDetailOption;";
            migrationBuilder.Sql(dropSelectNewProduct);

        }
    }
}
