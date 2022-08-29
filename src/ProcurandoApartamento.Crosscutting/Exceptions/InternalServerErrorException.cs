using ProcurandoApartamento.Crosscutting.Constants;

namespace ProcurandoApartamento.Crosscutting.Exceptions
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException(string message) : base(ErrorConstants.DefaultType, message)
        {
        }
    }
}
