using ProJAK.Domain.Entities;
using ProJAK.Repository.IRepository;
using ProJAK.Repository.Repository;
using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.GraphicsCardDto;
using ProJAK.Service.IService;

namespace ProJAK.Service.Service
{
    public class GraphicsCardService : IGraphicsCardService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GraphicsCardService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<Response<object>> AddGraphicsCardAsync(GraphicsCardDto addGraphicsCardDto)
        {
            try
            {
                GraphicsCard newGraphicsCard = new GraphicsCard
                {
                    Name = addGraphicsCardDto.Name,
                    ManufacturerId = addGraphicsCardDto.ManufacturerId,
                    MemorySize = addGraphicsCardDto.MemorySize
                };

                var result = await _unitOfWork.GraphicsCards.AddAsync(newGraphicsCard);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to add graphicsCard.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save graphicsCard.");
                }

                return Response<object>.Created("GraphicsCard added successfully.");
            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while adding the graphicsCard.", new List<string> { ex.Message });
            }
        }
        public async Task<Response<List<GraphicsCardDto>>> GetAllGraphicsCardAsync()
        {
            try
            {
                var GraphicsCardEntities = await _unitOfWork.GraphicsCards.GetAllAsync();
                if (!GraphicsCardEntities.Any())
                {
                    return Response<List<GraphicsCardDto>>.NoContent();
                }
                var graphicsCardEntitieDtos = GraphicsCardEntities.Select(GraphicsCardEntitie => new GraphicsCardDto
                {
                    Id = GraphicsCardEntitie.Id,
                    Name = GraphicsCardEntitie.Name,
                    MemorySize = GraphicsCardEntitie.MemorySize,
                    ManufacturerId = GraphicsCardEntitie.ManufacturerId
                }).ToList();
                return Response<List<GraphicsCardDto>>.Success(graphicsCardEntitieDtos, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<GraphicsCardDto>>.ServerError("An error occurred while get the graphicsCard.", new List<string> { ex.Message });
            }
        }

        public async Task<Response<GraphicsCardDto>> GetGraphicsCardByIdAsync(Guid graphicsCardId)
        {
            try
            {
                var graphicsCardEntity = await _unitOfWork.GraphicsCards.GetByIdAsync(graphicsCardId);

                if (graphicsCardEntity == null)
                    return Response<GraphicsCardDto>.NoContent();

                GraphicsCardDto graphicsCardDto = new GraphicsCardDto
                {
                    Id = graphicsCardEntity.Id,
                    Name = graphicsCardEntity.Name,
                    MemorySize = graphicsCardEntity.MemorySize,
                    ManufacturerId = graphicsCardEntity.ManufacturerId
                };
                return Response<GraphicsCardDto>.Success(graphicsCardDto, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<GraphicsCardDto>.ServerError("An error occurred while get the graphicsCard.", new List<string> { ex.Message });
            }
        }

        public async Task<Response<object>> UpdateGraphicsCardAsync(GraphicsCardDto updateGraphicsCardDto)
        {
            try
            {
                var oldGraphicsCard = await _unitOfWork.GraphicsCards.GetByIdAsync(updateGraphicsCardDto.Id);
                if (oldGraphicsCard == null)
                {
                    return Response<object>.BadRequest("GraphicsCard not found.");
                }
                oldGraphicsCard.Name = updateGraphicsCardDto.Name;
                oldGraphicsCard.MemorySize = updateGraphicsCardDto.MemorySize;
                oldGraphicsCard.ManufacturerId = updateGraphicsCardDto.ManufacturerId;
                var result = await _unitOfWork.GraphicsCards.UpdateAsync(oldGraphicsCard);
                if (result == null)
                {
                    return Response<object>.BadRequest("Failed to update graphicsCard.");
                }

                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to save graphicsCard.");
                }

                return Response<object>.Updated("GraphicsCard updateed successfully.");


            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while update the graphicsCard.", new List<string> { ex.Message });
            }
        }
        public async Task<Response<object>> DeleteGraphicsCardAsync(Guid graphicsCardId)
        {
            try
            {
                var oldGraphicsCard = await _unitOfWork.GraphicsCards.GetByIdAsync(graphicsCardId);
                if (oldGraphicsCard == null)
                {
                    return Response<object>.BadRequest("GraphicsCard not found with the given ID.");
                }
                await _unitOfWork.GraphicsCards.DeleteAsync(oldGraphicsCard);
                var save = await _unitOfWork.SaveAsync();
                if (!save)
                {
                    return Response<object>.BadRequest("Failed to delete graphicsCard.");
                }

                return Response<object>.Created("GraphicsCard deleteed successfully.");

            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while delete the graphicsCard.", new List<string> { ex.Message });
            }
        }
    }
}
