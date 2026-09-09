using BookFlow.BLL.Exceptions;

namespace BookFlow.BLL.Helpers;

public static class ValidationHelper
{
    public static void ValidateId(int id)
    {
        if (id <= 0)
            throw new BadRequestException(
                "The identifier must be greater than zero");
    }
}