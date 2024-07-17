import axios from "axios";

export const fetchCustomers = async () => {
  console.log("Fetching customers..."); // Лог для перевірки виклику функції
  try {
    const response = await axios.get("https://localhost:7018/api/customers/GetAllCustomers");
    console.log(response.data.value);
    return response.data.value;
  } catch (e) {
    console.error(e);
  }
}
