import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from './store';
import store from "../../index";
import { Button } from 'react-bootstrap'

class Calculate extends Component {

    constructor(props) {
        super(props);

        this.state = {
            agePlusTwenty: {},
            topColours: {},
            isLoading: false
        }

        this.submitTest = this.submitTestBtnHandler.bind(this);
    }

    componentWillMount() {
        this.props.requestCalculatedData();
    }

    submitTestBtnHandler(e) {
        const loading = store.getState().calculatedDataStore.isLoading;

        if (loading === false) {

            let ages = [...store.getState().calculatedDataStore.agePlusTwenty];
            let colours = [...store.getState().calculatedDataStore.topColours];

            let objectToPost = {
                agePlusTwenty: ages,
                topColours: colours
            };

            const response = fetch(`https://recruitment.highfieldqualifications.com/api/SubmitTest`,
            {
                method: 'POST',
                mode: 'no-cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                },
                body: JSON.stringify({ objectToPost })
            }).catch(function (err) {
                console.log(err);
            });
        }
    }

    render() {
        return (
            <div>
                <div className="row text-center">
                    <img src={require('../../images/logo.jpg')} alt="Logo" height="150" width="150"></img>
                </div>
                <Button type="button"
                    onClick={this.submitTest}
                    className="pull-right"
                    bsStyle="success">
                        Submit
                </Button>
                {renderLoading(this.props)}
                {renderTopColours(this.props)}
                {renderAges(this.props)}
            </div>
        );
    }
}

function renderAges(props) {
    let arr = [...props.agePlusTwenty]

    return (
        <table className='table p-2'>
            <thead>
                <tr>
                    <th>Ages</th>
                </tr>
            </thead>
            <tbody>
                {arr.map((value, index) =>
                    <tr key={index}>
                        <td>{value}</td>
                    </tr>
                )}
            </tbody>
        </table>
    );
}

function renderTopColours(props) {
    let arr = [...props.topColours]

    return (
        <table className='table'>
            <thead>
                <tr>
                    <th>Colour</th>
                    <th>Amount</th>
                </tr>
            </thead>
            <tbody>
                {arr.map((value, index) =>
                    <tr key={index}>
                        <td>{value.colour}</td>
                        <td>{value.amount}</td>
                    </tr>
                )}
            </tbody>
        </table>
    );
}

function renderLoading(props) {
    return <p className='clearfix text-center'>
        {props.isLoading ? <span>Loading...</span> : []}
    </p>;
}

function mapStateToProps(state) {
    return {
        agePlusTwenty: state.calculatedDataStore.agePlusTwenty,
        topColours: state.calculatedDataStore.topColours,
        isLoading: false
    };
}

export default connect(
    state => mapStateToProps(state),
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Calculate);
