using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;

namespace Instrument_Spec_Manager.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
public class Instrument_Spec_ManagerContextInitializer : DbContextTypesInfoInitializerBase {
	protected override DbContext CreateDbContext() {
		var optionsBuilder = new DbContextOptionsBuilder<Instrument_Spec_ManagerEFCoreDbContext>()
            //.UseSqlServer(";")
            .UseNpgsql(";")
            .UseChangeTrackingProxies()
            .UseObjectSpaceLinkProxies();
        return new Instrument_Spec_ManagerEFCoreDbContext(optionsBuilder.Options);
	}
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class Instrument_Spec_ManagerDesignTimeDbContextFactory : IDesignTimeDbContextFactory<Instrument_Spec_ManagerEFCoreDbContext> {
	public Instrument_Spec_ManagerEFCoreDbContext CreateDbContext(string[] args) {
		//throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
		var optionsBuilder = new DbContextOptionsBuilder<Instrument_Spec_ManagerEFCoreDbContext>();
        //optionsBuilder.UseSqlServer("Integrated Security=SSPI;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Instrument_Spec_Manager");
        optionsBuilder.UseNpgsql("Host=ambsim;Port=5432;Database=dev_sample;Username=postgres;Password=Ambric24");
        optionsBuilder.UseChangeTrackingProxies();
        optionsBuilder.UseObjectSpaceLinkProxies();
        return new Instrument_Spec_ManagerEFCoreDbContext(optionsBuilder.Options);
    }
}
[TypesInfoInitializer(typeof(Instrument_Spec_ManagerContextInitializer))]
public class Instrument_Spec_ManagerEFCoreDbContext : DbContext {
	public Instrument_Spec_ManagerEFCoreDbContext(DbContextOptions<Instrument_Spec_ManagerEFCoreDbContext> options) : base(options) {
	}
	//public DbSet<ModuleInfo> ModulesInfo { get; set; }
	public DbSet<ModelDifference> ModelDifferences { get; set; }
	public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
	public DbSet<PermissionPolicyRole> Roles { get; set; }
	public DbSet<Instrument_Spec_Manager.Module.BusinessObjects.ApplicationUser> Users { get; set; }
    public DbSet<Instrument_Spec_Manager.Module.BusinessObjects.ApplicationUserLoginInfo> UserLoginInfos { get; set; }
    public DbSet<Instrument_Spec_Manager.Module.BusinessObjects.Employee> Employees { get; set; }
    public DbSet<Instrument_Spec_Manager.Module.BusinessObjects.Department> Departments { get; set; }
    public DbSet<Instrument_Spec_Manager.Module.BusinessObjects.DemoTask> DemoTasks { get; set; }
    public DbSet<Instrument_Spec_Manager.Module.BusinessObjects.Position> Positions { get; set; }
    public DbSet<Instrument_Spec_Manager.Module.BusinessObjects.PhoneNumber> PhoneNumbers { get; set; }
    public DbSet<Instrument_Spec_Manager.Module.BusinessObjects.Address> Addresses { get; set; }
    public DbSet<Instrument_Spec_Manager.Module.BusinessObjects.Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.SetOneToManyAssociationDeleteBehavior(DeleteBehavior.SetNull, DeleteBehavior.Cascade);
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        modelBuilder.Entity<Instrument_Spec_Manager.Module.BusinessObjects.ApplicationUserLoginInfo>(b => {
            b.HasIndex(nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName), nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
        });
        modelBuilder.Entity<ModelDifference>()
            .HasMany(t => t.Aspects)
            .WithOne(t => t.Owner)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
