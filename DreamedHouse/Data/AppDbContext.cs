using DreamedHouse.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamedHouse.Data;

public partial class AppDbContext : DbContext
{
	public virtual DbSet<FinishingHouse> FinishingHouses { get; set; }

	public virtual DbSet<House> Houses { get; set; }

	public virtual DbSet<ProformaInvoice> ProformaInvoices { get; set; }

	public virtual DbSet<Role> Roles { get; set; }

	public virtual DbSet<User> Users { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder
			.UseCollation("utf8mb4_0900_ai_ci")
			.HasCharSet("utf8mb4");

		modelBuilder.Entity<FinishingHouse>(entity =>
		{
			entity.HasKey(e => e.FinishingHouseId)
				.HasName("PRIMARY");

			entity.ToTable("finishing_houses");

			entity.Property(e => e.FinishingHouseId)
				.HasColumnName("finishing_house_id");

			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP")
				.HasColumnType("timestamp")
				.HasColumnName("created_at");

			entity.Property(e => e.Name)
				.HasMaxLength(100)
				.HasColumnName("name");

			entity.Property(e => e.Price)
				.HasColumnType("double(10,2)")
				.HasColumnName("price");

			entity.Property(e => e.TypeFinishing)
				.HasMaxLength(30)
				.HasColumnName("type_finishing");

			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP")
				.HasColumnType("timestamp")
				.HasColumnName("updated_at");
		});

		modelBuilder.Entity<House>(entity =>
		{
			entity.HasKey(e => e.HouseId)
				.HasName("PRIMARY");

			entity.ToTable("houses");

			entity.Property(e => e.HouseId)
				.HasColumnName("house_id");

			entity.Property(e => e.BathroomsNumber)
				.HasColumnName("bathrooms_number");

			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP")
				.HasColumnType("timestamp")
				.HasColumnName("created_at");

			entity.Property(e => e.FloorsNumber)
				.HasColumnName("floors_number");

			entity.Property(e => e.Image)
				.HasMaxLength(255)
				.HasColumnName("image");

			entity.Property(e => e.Name)
				.HasMaxLength(100)
				.HasColumnName("name");

			entity.Property(e => e.Price)
				.HasColumnType("double(10,2)")
				.HasColumnName("price");

			entity.Property(e => e.RoomsNumber)
				.HasColumnName("rooms_number");

			entity.Property(e => e.SquareMeters)
				.HasColumnName("square_meters");

			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP")
				.HasColumnType("timestamp")
				.HasColumnName("updated_at");
		});

		modelBuilder.Entity<ProformaInvoice>(entity =>
		{
			entity.HasKey(e => e.ProformaInvoiceId)
				.HasName("PRIMARY");

			entity.ToTable("proforma_invoices");

			entity.HasIndex(e => e.HouseId, "fk_proforma_invoices_houses");

			entity.HasIndex(e => e.UserId, "fk_proforma_invoices_users");

			entity.Property(e => e.ProformaInvoiceId)
				.HasColumnName("proforma_invoice_id");

			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP")
				.HasColumnType("timestamp")
				.HasColumnName("created_at");

			entity.Property(e => e.HouseId)
				.HasColumnName("house_id");

			entity.Property(e => e.Total)
				.HasColumnType("double(10,2)")
				.HasColumnName("total");

			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP")
				.HasColumnType("timestamp")
				.HasColumnName("updated_at");

			entity.Property(e => e.UserId)
				.HasColumnName("user_id");

			entity.HasOne(d => d.House)
				.WithMany()
				.HasForeignKey(d => d.HouseId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("fk_proforma_invoices_houses");

			entity.HasOne(d => d.User)
				.WithMany()
				.HasForeignKey(d => d.UserId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("fk_proforma_invoices_users");
		});

		modelBuilder.Entity<ProformaInvoiceFinishingHouse>(entity =>
		{
			entity.HasKey(e => new { e.ProformaInvoiceId, e.FinishingHouseId })
				.HasName("PRIMARY");

			entity.ToTable("proforma_invoices_finishing_houses");

			entity.HasIndex(e => e.ProformaInvoiceId, "fk_proforma_invoices_finishing_houses_proforma_invoices");

			entity.HasIndex(e => e.FinishingHouseId, "fk_proforma_invoices_finishing_houses_finishing_houses");

			entity.Property(e => e.ProformaInvoiceId)
				.HasColumnName("proforma_invoice_id");

			entity.Property(e => e.FinishingHouseId)
				.HasColumnName("finishing_house_id");

			entity.HasOne(d => d.ProformaInvoice)
				.WithMany(p => p.ProformaInvoicesFinishingHouses)
				.HasForeignKey(d => d.ProformaInvoiceId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("fk_proforma_invoices_finishing_houses_proforma_invoices");

			entity.HasOne(d => d.FinishingHouse)
				.WithMany(p => p.ProformaInvoicesFinishingHouses)
				.HasForeignKey(d => d.FinishingHouseId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("fk_proforma_invoices_finishing_houses_finishing_houses");
		});

		modelBuilder.Entity<Role>(entity =>
		{
			entity.HasKey(e => e.RoleId)
				.HasName("PRIMARY");

			entity.ToTable("roles");

			entity.Property(e => e.RoleId)
				.HasColumnName("role_id");

			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP")
				.HasColumnType("timestamp")
				.HasColumnName("created_at");

			entity.Property(e => e.Name)
				.HasMaxLength(100)
				.HasColumnName("name");

			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP")
				.HasColumnType("timestamp")
				.HasColumnName("updated_at");
		});

		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(e => e.UserId)
				.HasName("PRIMARY");

			entity.ToTable("users");

			entity.HasIndex(e => e.Dni, "dni")
				.IsUnique();

			entity.HasIndex(e => e.Email, "email")
				.IsUnique();

			entity.HasIndex(e => e.RoleId, "fk_users_roles");

			entity.HasIndex(e => e.PhoneNumber, "phone_number")
				.IsUnique();

			entity.Property(e => e.UserId)
				.HasColumnName("user_id");

			entity.Property(e => e.BirthDate)
				.HasColumnName("birth_date");

			entity.Property(e => e.CreatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP")
				.HasColumnType("timestamp")
				.HasColumnName("created_at");

			entity.Property(e => e.Dni)
				.HasMaxLength(10)
				.HasColumnName("dni");

			entity.Property(e => e.Email)
				.HasMaxLength(50)
				.HasColumnName("email");

			entity.Property(e => e.FirstName)
				.HasMaxLength(50)
				.HasColumnName("first_name");

			entity.Property(e => e.LastName)
				.HasMaxLength(50)
				.HasColumnName("last_name");

			entity.Property(e => e.Password)
				.HasMaxLength(60)
				.HasColumnName("password");

			entity.Property(e => e.PhoneNumber)
				.HasMaxLength(10)
				.HasColumnName("phone_number");

			entity.Property(e => e.RoleId)
				.HasColumnName("role_id");

			entity.Property(e => e.UpdatedAt)
				.HasDefaultValueSql("CURRENT_TIMESTAMP")
				.HasColumnType("timestamp")
				.HasColumnName("updated_at");

			entity.HasOne(d => d.Role)
				.WithMany(p => p.Users)
				.HasForeignKey(d => d.RoleId)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("fk_users_roles");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

	public DbSet<DreamedHouse.Models.ProformaInvoiceFinishingHouse> ProformaInvoiceFinishingHouse { get; set; } = default!;
}
