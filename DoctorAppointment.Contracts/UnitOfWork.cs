namespace DoctorAppointment.Contracts;
public interface UnitOfWork
{
    Task Beging();
    Task Complete();
    Task Commiit();
    Task RollBack();
}
