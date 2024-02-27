using DoctorAppointment.Contracts;

namespace DoctorAppointment.Persistence.EF;
public class EFUnitOfWork : UnitOfWork
{

    private readonly EFDataContext _db;

    public EFUnitOfWork(EFDataContext db)
    {
        _db = db;
    }

    public async Task Beging()
    {
        await _db.Database.BeginTransactionAsync();

    }

    public async Task Commiit()
    {
        await _db.Database.CommitTransactionAsync();
    }

    public async Task Complete()
    {
        await _db.SaveChangesAsync();
    }

    public async Task RollBack()
    {
        await  _db.Database.RollbackTransactionAsync();
    }
}
