using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Mes.Shopfloor.Api.ProductionManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "resources");

            migrationBuilder.EnsureSchema(
                name: "product_definition");

            migrationBuilder.EnsureSchema(
                name: "scheduling");

            migrationBuilder.EnsureSchema(
                name: "data_collection");

            migrationBuilder.CreateTable(
                name: "equipment",
                schema: "resources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "manufacturing_plant",
                schema: "resources",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manufacturing_plant", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "material",
                schema: "product_definition",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sku = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_material", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "part",
                schema: "product_definition",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sku = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_part", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "production_order",
                schema: "scheduling",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_order", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "production_process",
                schema: "product_definition",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_process", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "production_unit_schedule",
                schema: "scheduling",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    production_unit_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_unit_schedule", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "production_unit_type",
                schema: "resources",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    state_group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reject_group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_unit_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionUnitType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionUnitType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "reject_group",
                schema: "data_collection",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reject_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "state_group",
                schema: "data_collection",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_state_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "status",
                schema: "data_collection",
                columns: table => new
                {
                    production_unit_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.production_unit_id);
                });

            migrationBuilder.CreateTable(
                name: "worker_group",
                schema: "resources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_worker_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "worker_qualification",
                schema: "resources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_worker_qualification", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "shopfloor",
                schema: "resources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    manufacturing_plant_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shopfloor", x => x.id);
                    table.ForeignKey(
                        name: "FK_shopfloor_manufacturing_plant_manufacturing_plant_id",
                        column: x => x.manufacturing_plant_id,
                        principalSchema: "resources",
                        principalTable: "manufacturing_plant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "production_order_progress",
                schema: "scheduling",
                columns: table => new
                {
                    production_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    target_quantity = table.Column<double>(type: "double precision", nullable: false),
                    produced_quantity = table.Column<double>(type: "double precision", nullable: false),
                    target_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    production_process_id = table.Column<Guid>(type: "uuid", nullable: true),
                    production_process_step_id = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_order_progress", x => x.production_order_id);
                    table.ForeignKey(
                        name: "FK_production_order_progress_production_order_production_order~",
                        column: x => x.production_order_id,
                        principalSchema: "scheduling",
                        principalTable: "production_order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "product_definition",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    production_process_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_production_process_production_process_id",
                        column: x => x.production_process_id,
                        principalSchema: "product_definition",
                        principalTable: "production_process",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "production_step",
                schema: "product_definition",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    production_process_id = table.Column<Guid>(type: "uuid", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    production_unit_group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    duration_deviation_seconds = table.Column<double>(type: "double precision", nullable: true),
                    duration_value = table.Column<TimeSpan>(type: "interval", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_step", x => x.id);
                    table.ForeignKey(
                        name: "FK_production_step_production_process_production_process_id",
                        column: x => x.production_process_id,
                        principalSchema: "product_definition",
                        principalTable: "production_process",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "production_unit_task",
                schema: "scheduling",
                columns: table => new
                {
                    production_schedule_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    production_unit_id = table.Column<Guid>(type: "uuid", nullable: false),
                    production_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    starting_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    completing_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_unit_task", x => x.production_schedule_Id);
                    table.ForeignKey(
                        name: "FK_production_unit_task_production_order_production_order_id",
                        column: x => x.production_order_id,
                        principalSchema: "scheduling",
                        principalTable: "production_order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_production_unit_task_production_unit_schedule_production_sc~",
                        column: x => x.production_schedule_Id,
                        principalSchema: "scheduling",
                        principalTable: "production_unit_schedule",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reject",
                schema: "data_collection",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    reject_group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    color = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reject", x => x.id);
                    table.ForeignKey(
                        name: "FK_reject_reject_group_reject_group_id",
                        column: x => x.reject_group_id,
                        principalSchema: "data_collection",
                        principalTable: "reject_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "state",
                schema: "data_collection",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    state_group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    IsIdle = table.Column<bool>(type: "boolean", nullable: false),
                    IsProductive = table.Column<bool>(type: "boolean", nullable: false),
                    code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    color = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_state", x => x.id);
                    table.ForeignKey(
                        name: "FK_state_state_group_state_group_id",
                        column: x => x.state_group_id,
                        principalSchema: "data_collection",
                        principalTable: "state_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "worker",
                schema: "resources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    number = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    first_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    last_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_worker", x => x.id);
                    table.ForeignKey(
                        name: "FK_worker_worker_group_group_id",
                        column: x => x.group_id,
                        principalSchema: "resources",
                        principalTable: "worker_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "production_unit_group_qualification",
                schema: "resources",
                columns: table => new
                {
                    production_unit_group_id = table.Column<int>(type: "integer", nullable: false),
                    worker_qualification_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_unit_group_qualification", x => new { x.worker_qualification_id, x.production_unit_group_id });
                    table.ForeignKey(
                        name: "FK_production_unit_group_qualification_production_unit_type_pr~",
                        column: x => x.production_unit_group_id,
                        principalSchema: "resources",
                        principalTable: "production_unit_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_production_unit_group_qualification_worker_qualification_wo~",
                        column: x => x.worker_qualification_id,
                        principalSchema: "resources",
                        principalTable: "worker_qualification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "worker_group_qualification",
                schema: "resources",
                columns: table => new
                {
                    worker_group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    worker_qualification_id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkerGroupId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_worker_group_qualification", x => new { x.worker_qualification_id, x.worker_group_id });
                    table.ForeignKey(
                        name: "FK_worker_group_qualification_worker_group_WorkerGroupId1",
                        column: x => x.WorkerGroupId1,
                        principalSchema: "resources",
                        principalTable: "worker_group",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_worker_group_qualification_worker_group_worker_group_id",
                        column: x => x.worker_group_id,
                        principalSchema: "resources",
                        principalTable: "worker_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_worker_group_qualification_worker_qualification_worker_qual~",
                        column: x => x.worker_qualification_id,
                        principalSchema: "resources",
                        principalTable: "worker_qualification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "production_line",
                schema: "resources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    shopfloor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_line", x => x.id);
                    table.ForeignKey(
                        name: "FK_production_line_shopfloor_shopfloor_id",
                        column: x => x.shopfloor_id,
                        principalSchema: "resources",
                        principalTable: "shopfloor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "production_step_equipment",
                schema: "product_definition",
                columns: table => new
                {
                    production_step_id = table.Column<Guid>(type: "uuid", nullable: false),
                    equipment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_step_equipment", x => new { x.production_step_id, x.equipment_id });
                    table.ForeignKey(
                        name: "FK_production_step_equipment_equipment_equipment_id",
                        column: x => x.equipment_id,
                        principalSchema: "resources",
                        principalTable: "equipment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_production_step_equipment_production_step_production_step_id",
                        column: x => x.production_step_id,
                        principalSchema: "product_definition",
                        principalTable: "production_step",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "production_step_material",
                schema: "product_definition",
                columns: table => new
                {
                    production_step_id = table.Column<Guid>(type: "uuid", nullable: false),
                    material_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_step_material", x => new { x.production_step_id, x.material_id });
                    table.ForeignKey(
                        name: "FK_production_step_material_material_material_id",
                        column: x => x.material_id,
                        principalSchema: "product_definition",
                        principalTable: "material",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_production_step_material_production_step_production_step_id",
                        column: x => x.production_step_id,
                        principalSchema: "product_definition",
                        principalTable: "production_step",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "production_step_parameter",
                schema: "product_definition",
                columns: table => new
                {
                    production_step_id = table.Column<Guid>(type: "uuid", nullable: false),
                    key = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    value = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_step_parameter", x => new { x.production_step_id, x.key });
                    table.ForeignKey(
                        name: "FK_production_step_parameter_production_step_production_step_id",
                        column: x => x.production_step_id,
                        principalSchema: "product_definition",
                        principalTable: "production_step",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "production_step_part",
                schema: "product_definition",
                columns: table => new
                {
                    production_step_id = table.Column<Guid>(type: "uuid", nullable: false),
                    part_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_step_part", x => new { x.production_step_id, x.part_id });
                    table.ForeignKey(
                        name: "FK_production_step_part_part_part_id",
                        column: x => x.part_id,
                        principalSchema: "product_definition",
                        principalTable: "part",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_production_step_part_production_step_production_step_id",
                        column: x => x.production_step_id,
                        principalSchema: "product_definition",
                        principalTable: "production_step",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "status_state",
                schema: "data_collection",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    production_unit_id = table.Column<Guid>(type: "uuid", nullable: false),
                    state_id = table.Column<Guid>(type: "uuid", nullable: false),
                    started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ended_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status_state", x => x.id);
                    table.ForeignKey(
                        name: "FK_status_state_state_state_id",
                        column: x => x.state_id,
                        principalSchema: "data_collection",
                        principalTable: "state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_status_state_status_production_unit_id",
                        column: x => x.production_unit_id,
                        principalSchema: "data_collection",
                        principalTable: "status",
                        principalColumn: "production_unit_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "production_unit",
                schema: "resources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    key = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    type_id = table.Column<int>(type: "integer", nullable: false),
                    group_id = table.Column<int>(type: "integer", nullable: false),
                    production_line_id = table.Column<Guid>(type: "uuid", nullable: true),
                    shopfloor_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_production_unit", x => x.id);
                    table.ForeignKey(
                        name: "FK_production_unit_ProductionUnitType_type_id",
                        column: x => x.type_id,
                        principalTable: "ProductionUnitType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_production_unit_production_line_production_line_id",
                        column: x => x.production_line_id,
                        principalSchema: "resources",
                        principalTable: "production_line",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_production_unit_production_unit_type_group_id",
                        column: x => x.group_id,
                        principalSchema: "resources",
                        principalTable: "production_unit_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_production_unit_shopfloor_shopfloor_id",
                        column: x => x.shopfloor_id,
                        principalSchema: "resources",
                        principalTable: "shopfloor",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_material_id",
                schema: "product_definition",
                table: "material",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_part_id",
                schema: "product_definition",
                table: "part",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_product_id",
                schema: "product_definition",
                table: "product",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_product_production_process_id",
                schema: "product_definition",
                table: "product",
                column: "production_process_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_line_shopfloor_id",
                schema: "resources",
                table: "production_line",
                column: "shopfloor_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_process_id",
                schema: "product_definition",
                table: "production_process",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_production_step_id",
                schema: "product_definition",
                table: "production_step",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_production_step_production_process_id",
                schema: "product_definition",
                table: "production_step",
                column: "production_process_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_step_equipment_equipment_id",
                schema: "product_definition",
                table: "production_step_equipment",
                column: "equipment_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_step_material_material_id",
                schema: "product_definition",
                table: "production_step_material",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_step_part_part_id",
                schema: "product_definition",
                table: "production_step_part",
                column: "part_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_unit_group_id",
                schema: "resources",
                table: "production_unit",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_unit_production_line_id",
                schema: "resources",
                table: "production_unit",
                column: "production_line_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_unit_shopfloor_id",
                schema: "resources",
                table: "production_unit",
                column: "shopfloor_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_unit_type_id",
                schema: "resources",
                table: "production_unit",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_unit_group_qualification_production_unit_group_id",
                schema: "resources",
                table: "production_unit_group_qualification",
                column: "production_unit_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_production_unit_task_production_order_id",
                schema: "scheduling",
                table: "production_unit_task",
                column: "production_order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reject_id",
                schema: "data_collection",
                table: "reject",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_reject_reject_group_id",
                schema: "data_collection",
                table: "reject",
                column: "reject_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_reject_group_id",
                schema: "data_collection",
                table: "reject_group",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_shopfloor_manufacturing_plant_id",
                schema: "resources",
                table: "shopfloor",
                column: "manufacturing_plant_id");

            migrationBuilder.CreateIndex(
                name: "IX_state_id",
                schema: "data_collection",
                table: "state",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_state_state_group_id",
                schema: "data_collection",
                table: "state",
                column: "state_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_state_group_id",
                schema: "data_collection",
                table: "state_group",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_status_state_production_unit_id",
                schema: "data_collection",
                table: "status_state",
                column: "production_unit_id");

            migrationBuilder.CreateIndex(
                name: "IX_status_state_state_id",
                schema: "data_collection",
                table: "status_state",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "IX_worker_group_id",
                schema: "resources",
                table: "worker",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_worker_group_qualification_worker_group_id",
                schema: "resources",
                table: "worker_group_qualification",
                column: "worker_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_worker_group_qualification_WorkerGroupId1",
                schema: "resources",
                table: "worker_group_qualification",
                column: "WorkerGroupId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product",
                schema: "product_definition");

            migrationBuilder.DropTable(
                name: "production_order_progress",
                schema: "scheduling");

            migrationBuilder.DropTable(
                name: "production_step_equipment",
                schema: "product_definition");

            migrationBuilder.DropTable(
                name: "production_step_material",
                schema: "product_definition");

            migrationBuilder.DropTable(
                name: "production_step_parameter",
                schema: "product_definition");

            migrationBuilder.DropTable(
                name: "production_step_part",
                schema: "product_definition");

            migrationBuilder.DropTable(
                name: "production_unit",
                schema: "resources");

            migrationBuilder.DropTable(
                name: "production_unit_group_qualification",
                schema: "resources");

            migrationBuilder.DropTable(
                name: "production_unit_task",
                schema: "scheduling");

            migrationBuilder.DropTable(
                name: "reject",
                schema: "data_collection");

            migrationBuilder.DropTable(
                name: "status_state",
                schema: "data_collection");

            migrationBuilder.DropTable(
                name: "worker",
                schema: "resources");

            migrationBuilder.DropTable(
                name: "worker_group_qualification",
                schema: "resources");

            migrationBuilder.DropTable(
                name: "equipment",
                schema: "resources");

            migrationBuilder.DropTable(
                name: "material",
                schema: "product_definition");

            migrationBuilder.DropTable(
                name: "part",
                schema: "product_definition");

            migrationBuilder.DropTable(
                name: "production_step",
                schema: "product_definition");

            migrationBuilder.DropTable(
                name: "ProductionUnitType");

            migrationBuilder.DropTable(
                name: "production_line",
                schema: "resources");

            migrationBuilder.DropTable(
                name: "production_unit_type",
                schema: "resources");

            migrationBuilder.DropTable(
                name: "production_order",
                schema: "scheduling");

            migrationBuilder.DropTable(
                name: "production_unit_schedule",
                schema: "scheduling");

            migrationBuilder.DropTable(
                name: "reject_group",
                schema: "data_collection");

            migrationBuilder.DropTable(
                name: "state",
                schema: "data_collection");

            migrationBuilder.DropTable(
                name: "status",
                schema: "data_collection");

            migrationBuilder.DropTable(
                name: "worker_group",
                schema: "resources");

            migrationBuilder.DropTable(
                name: "worker_qualification",
                schema: "resources");

            migrationBuilder.DropTable(
                name: "production_process",
                schema: "product_definition");

            migrationBuilder.DropTable(
                name: "shopfloor",
                schema: "resources");

            migrationBuilder.DropTable(
                name: "state_group",
                schema: "data_collection");

            migrationBuilder.DropTable(
                name: "manufacturing_plant",
                schema: "resources");
        }
    }
}
