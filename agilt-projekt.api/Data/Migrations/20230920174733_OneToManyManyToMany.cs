using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace agilt_projekt.api.Data.Migrations
{
    /// <inheritdoc />
    public partial class OneToManyManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendents",
                columns: table => new
                {
                    AttendentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendents", x => x.AttendentId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "AttendentModelEventModel",
                columns: table => new
                {
                    AttendentsAttendentId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventListEventId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendentModelEventModel", x => new { x.AttendentsAttendentId, x.EventListEventId });
                    table.ForeignKey(
                        name: "FK_AttendentModelEventModel_Attendents_AttendentsAttendentId",
                        column: x => x.AttendentsAttendentId,
                        principalTable: "Attendents",
                        principalColumn: "AttendentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendentModelEventModel_Events_EventListEventId",
                        column: x => x.EventListEventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendentModelEventModel_EventListEventId",
                table: "AttendentModelEventModel",
                column: "EventListEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendentModelEventModel");

            migrationBuilder.DropTable(
                name: "Attendents");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
