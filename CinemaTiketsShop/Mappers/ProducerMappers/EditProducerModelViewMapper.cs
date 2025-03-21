﻿using CinemaTiketsShop.Models;
using CinemaTiketsShop.ViewModels.ProducerVMs;

namespace CinemaTiketsShop.Mappers.ProducerMappers
{
    public static class EditProducerModelViewMapper
    {
        public static EditProducerViewModel MapEditProducerVM(this Producer ProducerModel)
        {
            return new EditProducerViewModel
            {
                Id = ProducerModel.Id,
                Name = ProducerModel.Name,
                Bio = ProducerModel.Bio,
                FotoUrl = ProducerModel.FotoURL,
                PublicId = ProducerModel.PublicId,
                OldPictureUrl = ProducerModel.FotoURL
            };
        }

        public static Producer MapProducerModel(this EditProducerViewModel ProducerEditVM)
        {
            return new Producer
            {
                Id = ProducerEditVM.Id,
                Name = ProducerEditVM.Name,
                Bio = ProducerEditVM.Bio,
                FotoURL = ProducerEditVM.OldPictureUrl,
                PublicId = ProducerEditVM.PublicId
            };
        }
    }
}
