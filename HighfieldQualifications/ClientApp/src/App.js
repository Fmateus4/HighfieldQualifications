import React, { Component } from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home/Home';
import CalculatedData from './components/CalculatedData/component'

export default () => (
  <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/calculate' component={CalculatedData} />
  </Layout>
);
