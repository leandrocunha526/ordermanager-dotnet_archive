using AutoMapper;
using ordermanager_dotnet.Entities;
using ordermanager_dotnet.Models;

namespace ordermanger_dotnet.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<User, UserModel>();
			CreateMap<RegisterModel, User>();
			CreateMap<UpdateUserModel, User>();
		}
	}
}
