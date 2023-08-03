using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rockaway.WebApp.Migrations {
	/// <inheritdoc />
	public partial class CreateObjectsForDomainModel : Migration {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.AlterDatabase(
				collation: "SQL_Latin1_General_CP1_CI_AI");

			migrationBuilder.AlterColumn<string>(
				name: "Description",
				table: "Artists",
				type: "nvarchar(1000)",
				maxLength: 1000,
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)");

			migrationBuilder.CreateTable(
				name: "Purchase",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					CustomerEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					CustomerPhone = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Purchase", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Venues",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
					City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					CultureCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
					Phone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
					PostalCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
					Website = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Venues", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Show",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					VenueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Date = table.Column<DateTime>(type: "datetime2", nullable: false),
					DoorsOpen = table.Column<DateTime>(type: "datetime2", nullable: false),
					StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
					FinishTime = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Show", x => x.Id);
					table.ForeignKey(
						name: "FK_Show_Venues_VenueId",
						column: x => x.VenueId,
						principalTable: "Venues",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Slot",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Slot", x => x.Id);
					table.ForeignKey(
						name: "FK_Slot_Artists_ArtistId",
						column: x => x.ArtistId,
						principalTable: "Artists",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Slot_Show_ShowId",
						column: x => x.ShowId,
						principalTable: "Show",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "TicketTypes",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ShowId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Price = table.Column<decimal>(type: "decimal(14,4)", precision: 14, scale: 4, nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_TicketTypes", x => x.Id);
					table.ForeignKey(
						name: "FK_TicketTypes_Show_ShowId",
						column: x => x.ShowId,
						principalTable: "Show",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Tickets",
				columns: table => new {
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					PurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					TicketTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_Tickets", x => x.Id);
					table.ForeignKey(
						name: "FK_Tickets_Purchase_PurchaseId",
						column: x => x.PurchaseId,
						principalTable: "Purchase",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Tickets_TicketTypes_TicketTypeId",
						column: x => x.TicketTypeId,
						principalTable: "TicketTypes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Show_VenueId",
				table: "Show",
				column: "VenueId");

			migrationBuilder.CreateIndex(
				name: "IX_Slot_ArtistId",
				table: "Slot",
				column: "ArtistId");

			migrationBuilder.CreateIndex(
				name: "IX_Slot_ShowId",
				table: "Slot",
				column: "ShowId");

			migrationBuilder.CreateIndex(
				name: "IX_Tickets_PurchaseId",
				table: "Tickets",
				column: "PurchaseId");

			migrationBuilder.CreateIndex(
				name: "IX_Tickets_TicketTypeId",
				table: "Tickets",
				column: "TicketTypeId");

			migrationBuilder.CreateIndex(
				name: "IX_TicketTypes_ShowId",
				table: "TicketTypes",
				column: "ShowId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "Slot");

			migrationBuilder.DropTable(
				name: "Tickets");

			migrationBuilder.DropTable(
				name: "Purchase");

			migrationBuilder.DropTable(
				name: "TicketTypes");

			migrationBuilder.DropTable(
				name: "Show");

			migrationBuilder.DropTable(
				name: "Venues");

			migrationBuilder.AlterDatabase(
				oldCollation: "SQL_Latin1_General_CP1_CI_AI");

			migrationBuilder.AlterColumn<string>(
				name: "Description",
				table: "Artists",
				type: "nvarchar(max)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(1000)",
				oldMaxLength: 1000);
		}
	}
}
