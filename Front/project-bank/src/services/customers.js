import axios from "axios";

export const fetchCustomers = async () => {
    try{
        var response = await axios.get("https://localhost:7018/api/customers/GetAllCustomers");
        console.log(response.data.value);
        return response.data.value;
    }
    catch (e) {
        console.error(e);
    }
}