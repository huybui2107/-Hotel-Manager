const bookingService = require('../service/bookingService');



module.exports = {
     
    createBooking :async( req,res,next) =>{
         try {
           
            const result = await bookingService.createBooking(req.body);
            console.log(result);
            res.status(200).json( result )
         } catch (error) {
            res.status(500).json({
                errCode :-1,
                errMessage: error.message,
            })
         }
    },
    getListBooking :async (req,res,next) =>{
        try {
           
            const {user_id,page} = req.query;
            const result = await bookingService.getListBooking(user_id,page);
            
            res.status(200).json( result )
         } catch (error) {
            res.status(500).json({
                errCode :-1,
                errMessage: error.message,
            })
         }
    },
    getListBookingByIdHotel : async (req,res,next) =>{
        try {
           
            
            const result = await bookingService.getListBookingByIdHotel(req.query);
            res.status(200).json( result )
         } catch (error) {
            res.status(500).json({
                errCode :-1,    
                errMessage: error.message,
            })
         }
    },
    updateStatus : async (req, res ,next) => {
        try {
            const result = await bookingService.updateStatus(req.body);
            res.status(200).json( result )
        } catch (error) {
            res.status(500).json({
                errCode :-1,    
                errMessage: error.message,
            })
        }
    },
    getRevenueByMonth :async (req, res) => {
        try {
        const {month,hotel_id} = req.query;
 
        const currentDate = new Date();
		let totalPrice = 0;
		const year = currentDate.getFullYear();;
		const countDay =await bookingService.countday(month,year);
		let Days = []
        console.log(countDay);
		for(let i=1;i<=countDay ;i++)
		{
			
			let date =year + "-" + month + "-" + i;
            let price = await bookingService.getTotalPriceByDate(hotel_id,date)
            console.log(price);
			Days.push({
                date: date,
                price : price[0].totalSum ? price[0].totalSum : 0,
            });
            price =parseInt(price[0].totalSum ? price[0].totalSum : 0)
            totalPrice += price;
			
		}
		
		
		//handle paid/unpaid
		const paid =await bookingService.countMaxItemByHotel(hotel_id, 3, month);
		const unPaid =await bookingService.countMaxItemByHotel(hotel_id, 4, month);
		// //handle tickets
		const tickets =await bookingService.countItemByMonth(hotel_id, month);
		
		
        res.status(200).json({
            tickets : tickets.count,
            totalprice : totalPrice,
            Days : Days,
            paid : paid.count,
            unPaid : unPaid.count,
        })
		
        } catch (error) {
            res.status(500).json({
                errCode :-1,    
                errMessage: error.message,
            })
        }
    }
}