const requestDataType = 'REQUEST_CALCULATED_DATA';
const receiveDataType = 'RECEIVE_CALCULATED_DATA';

const initialState = {
    agePlusTwenty: {},
    topColours: {},
    isLoading: false
};

export const actionCreators = {
    requestCalculatedData: () => async (dispatch) => {

        dispatch({ type: requestDataType });

        const url = `api/data/calculate`;
        const response = await fetch(url);
        const calculatedValue = await response.json();

        dispatch({ type: receiveDataType, calculatedValue });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestDataType) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === receiveDataType) {
        return {
            ...state,
            agePlusTwenty: [...action.calculatedValue.agePlusTwenty],
            topColours: [...action.calculatedValue.topColours],
            isLoading: false,
        };
    }

    return state;
};
