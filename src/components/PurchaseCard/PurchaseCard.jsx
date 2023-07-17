import { Col, Row, Typography } from "antd";
import { formatDate, formatMoney } from "../../utils/helper";

const PurchaseCard = ({ purchase }) => {
  
  return (
    <>
      <Row gutter={[24, 24]} className="px-5 py-10 rounded bg-gray-100 mt-4">
        <Col sm={4}>
          <Typography.Text>{purchase.hotel.hotel_name}</Typography.Text>
        </Col>
        <Col sm={6}>
          <Typography.Text>{purchase.hotel.hotel_address}</Typography.Text>
        </Col>
        <Col sm={6}>
          <Typography.Text>{purchase.room.name}</Typography.Text>
        </Col>
        <Col sm={5}>
          <Typography.Text>
            {formatDate(purchase.checkinDate)} /
            {formatDate(purchase.checkoutDate)}
          </Typography.Text>
        </Col>

        <Col sm={3}>
          <Typography.Text>{formatMoney(purchase.totalprice)}</Typography.Text>
        </Col>
      </Row>
    </>
  );
};

export default PurchaseCard;
