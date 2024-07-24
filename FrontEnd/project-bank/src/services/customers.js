import axios from "axios";

export const fetchCustomers = async () => {
  console.log("Fetching customers..."); // Лог для перевірки виклику функції
  try {
    const response = await axios.get("https://localhost:7018/api/customers");
    console.log(response.data.value);
    return response.data.value;
  } catch (e) {
    console.error(e);
  }
}

export const createCustomers = async (customer) => {
  console.log("Create....");
  try {
    const customerjson = {
      "name": customer.firstName,
      "lastName": customer.lastName,
      "country": customer.country,
      "phone": customer.phoneNumber,
      "email": customer.email
    }
    const response = await axios.post("https://localhost:7018/api/customers", customerjson);
    console.log(response.status);
    return response.status;
  } catch (e) {
    console.error(e);
  }
}