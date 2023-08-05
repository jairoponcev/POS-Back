using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.User.Request;
using POS.Application.Interfaces;
using POS.Application.Validators.User;
using POS.Domain.Entities;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Static;
using WatchDog;
using BC = BCrypt.Net.BCrypt;

namespace POS.Application.Services
{
    public class UserApplication : IUserApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserValidator _validatorRules;

        public UserApplication(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserValidator validatorRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validatorRules = validatorRules;
        }

        public async Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();

            try
            {
                var validationResult = await _validatorRules.ValidateAsync(requestDto);

                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;

                    return response;
                }

                var account = _mapper.Map<User>(requestDto);
                account.Password = BC.HashPassword(requestDto.Password);

                if (requestDto.Image is not null)
                {
                    account.Image = await _unitOfWork.Storage.SaveFile(AzureContainers.USERS, requestDto.Image);
                }

                response.Data = await _unitOfWork.User.RegisterAsync(account);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_SAVE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;

                WatchLogger.Log(ex.Message);
            }

            return response;
        }
    }
}
