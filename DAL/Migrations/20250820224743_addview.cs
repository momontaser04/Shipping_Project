using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW VwCities
AS
SELECT 
    dbo.TbCities.Id, 
    dbo.TbCities.CityAName, 
    dbo.TbCities.CityEName, 
    dbo.TbCities.CountryId, 
    dbo.TbCountries.CountryAName, 
    dbo.TbCountries.CountryEName, 
    dbo.TbCities.UpdatedBy, 
    dbo.TbCities.CurrentState, 
    dbo.TbCities.CreatedDate, 
    dbo.TbCities.CreatedBy, 
    dbo.TbCities.UpdatedDate
FROM dbo.TbCities 
INNER JOIN dbo.TbCountries 
    ON dbo.TbCities.CountryId = dbo.TbCountries.Id;
");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
