using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalkssAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultyandRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0266074e-cc21-4a13-8d8a-24f0e85ccb6c"), "Easy" },
                    { new Guid("283568c0-9ab1-47c6-a884-bb889b167750"), "Hard" },
                    { new Guid("3f82955a-5375-422c-9e29-077ef19d53d3"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Reagions",
                columns: new[] { "Id", "Code", "Name", "RegionImageURL" },
                values: new object[,]
                {
                    { new Guid("02c2fae2-2957-4fb5-8ddf-f67b90bc129d"), "RDP", "Roodeport", "https://www.sa-venues.com/maps/atlas/gau_roodepoort.gif" },
                    { new Guid("272cfc74-77f0-49eb-aad6-b11283cdaa0d"), "SDT", "Sandton", "https://th.bing.com/th/id/OIP.MI2hagCeMic8FNoH32HnCgAAAA?rs=1&pid=ImgDetMain" },
                    { new Guid("fed490ce-1f12-4e1e-8aea-160ffcfea7e4"), "MRD", "Midrand", "https://www.sa-venues.com/maps/atlas/gau_midrand.gif" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0266074e-cc21-4a13-8d8a-24f0e85ccb6c"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("283568c0-9ab1-47c6-a884-bb889b167750"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("3f82955a-5375-422c-9e29-077ef19d53d3"));

            migrationBuilder.DeleteData(
                table: "Reagions",
                keyColumn: "Id",
                keyValue: new Guid("02c2fae2-2957-4fb5-8ddf-f67b90bc129d"));

            migrationBuilder.DeleteData(
                table: "Reagions",
                keyColumn: "Id",
                keyValue: new Guid("272cfc74-77f0-49eb-aad6-b11283cdaa0d"));

            migrationBuilder.DeleteData(
                table: "Reagions",
                keyColumn: "Id",
                keyValue: new Guid("fed490ce-1f12-4e1e-8aea-160ffcfea7e4"));
        }
    }
}
