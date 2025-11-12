using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RouteG04.DAL.Data.Migrtations
{
    /// <inheritdoc />
    public partial class AddImageNamePropertyToEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Employee");
        }
    }
}
