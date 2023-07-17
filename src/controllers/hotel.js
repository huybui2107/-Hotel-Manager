const hotelService = require('../service/hotelService');


module.exports = {
    updateHotel :async (req,res,next) =>{
        try {
            const mess = await hotelService.updateHotel(req.body);
            console.log(mess);
            res.status(200).json( mess.hotel )
        } catch (error) {
            res.status(500).json({
                errCode :-1,
                errMessage: error.message,
            })
        }
    },
    searchHotel :async (req, res, next) => {
            try {
                const hotelIds = [];
             
                let newHotels = [];
         
            const result = await hotelService.searchHotel(req.query)
            if (result.hotels){
             result.hotels.map(hotel => {
                if (!hotelIds.includes(hotel.id)){
                    hotelIds.push(hotel.id);
                newHotels.push( {
                        user_id : hotel.user_id,
                        province_id :hotel.province_id, 
                        hotelName   :hotel.hotel_name ,        
                        hotelDescription : hotel.hotel_desc    ,    
                        hotelAddress   : hotel.hotel_address,
                        image           :  hotel.image,   
                        roomQuantity            : hotel.room_quantity,
                        hotelEmail          :  hotel.hotel_email,         
                        hotelPhone          :  hotel.hotel_phone,     
                        id          : hotel.id,
                } )
                
            }
            }) } else { newHotels = result.hotels }
            
            res.status(200).json( {
                maxPageItem : result.maxPageItem,
                hotels :newHotels ,
                totalPage: result.totalPage,
                page: result.page,
            })
            } catch (error) {
                res.status(500).json({
                    errCode :-1,
                    errMessage: error.message,
                })
            }
    },
    // /////  ROOM
    createRoom :async(req,res,next)  =>{
        try {
            const mess = await hotelService.createRoom(req.body);
            console.log(mess);
            res.status(200).json( mess.room)
        } catch (error) {
            res.status(500).json({
                errCode :-1,
                errMessage: error.message,
            })
        }
    },
    searchRoomAll :async (req, res) => {
        try {
            const {hotel_id , page} = req.query;
            console.log(hotel_id);
            const hotelRooms = await hotelService.searchRoomAll(hotel_id,page)
            res.status(200).json( hotelRooms)
        } catch (error) {
            res.status(500).json({
                errCode :-1,
                errMessage: error.message,
            })
        }
    },
    searchRoomById : async (req, res) => { 
        try {
            
            let rooms ;
            const result = await hotelService.searchRoomById(req.query)
            if (result.hotelRooms){
                rooms = result.hotelRooms.map(room => {
                    return {
                        id: room.id ,
                        hotel_id:room.hotel_id,
                        type_id:room.type_id ,
                        name:room.name ,
                        bed_quantity: room.bed_quantity,
                        price: room.price,
                        description: room.description,
                        image:room.image
                    }
                }) } else { rooms = result.hotelRooms}
                res.status(200).json( {
                    maxPageItem : result.maxPageItem,
                    rooms :rooms ,
                    totalPage: result.totalPage,
                    page: result.page,
                })
        } catch (error) {
            res.status(500).json({
                errCode :-1,
                errMessage: error.message,
            })
        }
    },
    updateRoom : async (req,res,next) => {
        try {
           
           const mess  = await hotelService.updateRoom(req.body);
           res.status(200).json( mess.room)
        } catch (error) {
            res.status(500).json({
                errCode :-1,
                errMessage: error.message,
            })
        }
    },
    deleteRoom :async (req, res) => {
        try {
           console.log(req.params.id)
           const mess  = await hotelService.deleteRoom(req.params.id);
           res.status(200).json( mess)
        } catch (error) {
            res.status(500).json({
                errCode :-1,
                errMessage: error.message,
            })
        }
    }
}