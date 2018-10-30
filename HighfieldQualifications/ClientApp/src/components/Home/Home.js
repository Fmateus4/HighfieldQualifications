import React, { Component } from 'react';
import { connect } from 'react-redux';

const Home = props => (
  <div>
        <h1>Interview Application</h1>
        <h4>Author: Fernando Mateus</h4>
  </div>
);

export default connect()(Home);
