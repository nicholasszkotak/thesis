import axios from "axios";
import {ADD_COMPUTER, DELETE_COMPUTER, GET_COMPUTERS, UPDATE_COMPUTER, SEARCH_COMPUTERS} from "./apiRoutes";

export const ComputerService = {

    getComputers: async () => {
        const computers = await axios.get(GET_COMPUTERS);
        return computers.data;
    },

    deleteComputer: async (computerId) => {
        await axios.delete(DELETE_COMPUTER(computerId));
    },

    saveComputer: async (computer) => {
        if(!computer.ports) computer.ports = [];
        
        if(computer.id)
            await axios.post(UPDATE_COMPUTER, computer);
        else
            await axios.put(ADD_COMPUTER, computer);
    },

    searchComputers: async (searchForm) => {
        const response = await axios.post(SEARCH_COMPUTERS, searchForm);
        return response.data;
    }
}