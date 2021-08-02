using AspNetCore.DAL;

namespace AspNetCore.IBLL
{
    public interface IStudentServiceGeneric
    {
       // public void PlayPhoneGeneric<T>(T phone) where T : AbstractPhone ;

        public void PlayPhoneGeneric(AbstractPhone phone);
    }
}
